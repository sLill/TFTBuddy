namespace TFTBuddy.Common
{
    /// <summary>
    /// Pub-Sub EventAggregator
    /// </summary>
    public interface IMessageProvider
    {
        #region Methods..
        /// <summary>
        /// Publishes an event of of type <typeparamref name="TEvent"/> to all subscribed listeners.
        /// </summary>
        void Publish<TEvent>(TEvent eventArgs);

        /// <summary>
        /// Subscribes a listener to an event of type <typeparamref name="TEvent"/>. <paramref name="action"/>
        /// parameter will be invoked when an event of type <typeparamref name="TEvent"/> is published.
        /// </summary>
        IDisposable Subscribe<TEvent>(Action<TEvent> action);

        /// <summary>
        /// Unsubscribes a listener from an event of type <typeparamref name="TEvent"/> by disposing of the subscription.
        /// </summary>
        void Unsubscribe<TEvent>(IDisposable subscription);
        #endregion Methods..
    }
}
