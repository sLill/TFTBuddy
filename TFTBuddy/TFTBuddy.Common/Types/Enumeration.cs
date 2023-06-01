using System.Reflection;

namespace TFTBuddy.Common
{
    public abstract class Enumeration : IComparable
    {
        #region Properties..
        public string Name { get; }
        public int Id { get; }
        #endregion Properties..

        #region Constructors..
        protected Enumeration(int id, string name)
        {
            Name = name;
            Id = id;
        }
        #endregion Constructors..

        #region Methods..
        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            FieldInfo[] fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public static bool TryGetFromValueOrName<T>(string valueOrName, out T? enumeration) where T : Enumeration
            => TryParse(item => item.Name == valueOrName, out enumeration) || int.TryParse(valueOrName, out var value) && TryParse(item => item.Id == value, out enumeration);

        public static T? FromValue<T>(int value) where T : Enumeration
            => Parse<T, int>(value, "nameOrValue", item => item.Id == value);

        public static T? FromName<T>(string name) where T : Enumeration
            => Parse<T, string>(name, "name", item => item.Name == name);

        private static bool TryParse<T>(Func<T, bool> predicate, out T? enumeration) where T : Enumeration
        {
            enumeration = GetAll<T>().FirstOrDefault(predicate);
            return enumeration != null;
        }

        private static T? Parse<T, TIntOrString>(TIntOrString nameOrValue, string description, Func<T, bool> predicate) where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);
            return matchingItem;
        }

        public int CompareTo(object? other)
        {
            if (other is Enumeration otherEnum)
                return Id.CompareTo(((Enumeration)other).Id);
            else
                throw new ArgumentException("Object is not Enumeration");
        }

        public override bool Equals(object? obj)
        {
            Enumeration? otherValue = obj as Enumeration;

            if (obj == null || otherValue == null)
                return false;

            bool typeMatches = GetType().Equals(obj.GetType());
            bool valueMatches = Id.Equals(otherValue.Id);

            bool isEqual = typeMatches && valueMatches;
            return isEqual;
        }

        public override int GetHashCode()
            => Id.GetHashCode();

        public override string ToString()
            => Name; 
        #endregion Methods..
    }
}
