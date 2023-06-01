using System.Collections.Concurrent;
using System.Reactive.Subjects;

namespace TFTBuddy.Common
{
    public class MessageProvider : IMessageProvider
    {
        #region Fields..
        private readonly ConcurrentDictionary<Type, object> _subjects = new();
        #endregion Fields..

        #region Methods..
        public void Publish<TEvent>(TEvent eventArgs)
        {
            var eventType = typeof(TEvent);
            if (_subjects.TryGetValue(eventType, out var subject))
                ((ISubject<TEvent>)subject).OnNext(eventArgs);
        }

        public IDisposable Subscribe<TEvent>(Action<TEvent> action)
        {
            var eventType = typeof(TEvent);

            if (!_subjects.TryGetValue(eventType, out var subject))
            {
                subject = new Subject<TEvent>();
                _subjects[eventType] = subject;
            }

            IDisposable subscription = ((ISubject<TEvent>)subject).Subscribe(action);
            return subscription;
        }

        public void Unsubscribe<TEvent>(IDisposable subscription)
            => subscription.Dispose();
        #endregion Methods..
    }
}
