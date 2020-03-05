using System.Text.RegularExpressions;

namespace ProjectStore.Core.DomainObjects
{
    public class AssertionConcern
    {
        public static void ValidateIfItsEqual(object object1, object object2, string message)
        {
            if (!object1.Equals(object2))
                throw new DomainException(message);
        }

        public static void ValidateIfNotEqual(object object1, object object2, string message)
        {
            if (object1.Equals(object2))
                throw new DomainException(message);
        }

        public static void ValidateCharacter(string value, int max, string message)
        {
            var lenght = value.Trim().Length;
            if (lenght > max)
                throw new DomainException(message);
        }

        public static void ValidateSize(string value, int min, int max, string message)
        {
            var lenght = value.Trim().Length;
            if (lenght < min || lenght > max)
                throw new DomainException(message);
        }

        public static void ValidateExpression(string pattern, string value, string message)
        {
            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
                throw new DomainException(message);
        }

        public static void ValidateIfItsEmptyOrNull(string value, string message)
        {
            if (string.IsNullOrEmpty(value.Trim()))
                throw new DomainException(message);
        }

        public static void ValidateIfItsNull(object object1, string message)
        {
            if (object1 == null)
                throw new DomainException(message);
        }

        public static void ValidateSize(double value, double min, double max, string message)
        {
            if (value < min || value > max)
                throw new DomainException(message);
        }

        public static void ValidateSize(float value, float min, float max, string message)
        {
            if (value < min || value > max)
                throw new DomainException(message);
        }

        public static void ValidateSize(int value, int min, int max, string message)
        {
            if (value < min || value > max)
                throw new DomainException(message);
        }

        public static void ValidateSize(long value, long min, long max, string message)
        {
            if (value < min || value > max)
                throw new DomainException(message);
        }

        public static void ValidateSize(decimal value, decimal min, decimal max, string message)
        {
            if (value < min || value > max)
                throw new DomainException(message);
        }

        public static void ValidateIfLessThanMin(long value, long min, string message)
        {
            if (value < min)
                throw new DomainException(message);
        }

        public static void ValidateIfLessThanMin(double value, double min, string message)
        {
            if (value < min)
                throw new DomainException(message);
        }

        public static void ValidateIfLessThanMin(decimal value, decimal min, string message)
        {
            if (value < min)
                throw new DomainException(message);
        }

        public static void ValidateIfLessThanMin(int value, int min, string message)
        {
            if (value < min)
                throw new DomainException(message);
        }

        public static void ValidateIfFalse(bool value, string message)
        {
            if (value)
                throw new DomainException(message);
        }

        public static void ValidateIfTrue(bool value, string message)
        {
            if (!value)
                throw new DomainException(message);
        }
    }
}