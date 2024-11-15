namespace MathematicalExpressions.operations.vectors
{
    public abstract class Vector<T>
    {
        public T[] Components { get; set; }
        public int Size => Components.Length;
        public abstract T Norm { get; }

        public Vector(int size)
        {
            ValidateSize(size);
            Components = new T[size];
        }

        public Vector(IEnumerable<T> components)
        {
            ValidateForNull(components);
            ValidateForEmptiness(components);
            Components = new T[components.Count()];
            components.ToArray().CopyTo(Components, 0);
        }

        public Vector(int size, IEnumerable<T> components)
        {
            ValidateSize(size);
            ValidateForNull(components);

            Components = new T[size];
            var componentsCount = components.Count();
            Array.Copy(components.ToArray(), Components, size < componentsCount ? size : componentsCount);
        }

        public Vector(Vector<T> vectorToCopy)
        {
            ValidateForNull(vectorToCopy);
            Components = new T[vectorToCopy.Size];
            vectorToCopy.Components.CopyTo(Components, 0);
        }

        public abstract void Add(Vector<T> vector);
        public abstract void Subtract(Vector<T> vector);
        public abstract void MultiplyByScalar(T scalar);
        public abstract void Reverse();

        public override bool Equals(object? obj)
        {
            if (this == obj) return true;
            if (obj == null || GetType() != obj.GetType()) return false;

            Vector<T> vector = (Vector<T>)obj;

            if (Size != vector.Size) return false;

            for (int i = 0; i < Size; ++i)
                if (!Components[i].Equals(vector.Components[i])) return false;
            return true;
        }
        public override string ToString() => $"{{{string.Join("; ", Components)}}}";

        protected static void ValidateSize(int size)
        { if (size <= 0) throw new ArgumentException(size.ToString()); }
        protected static void ValidateForNull(object obj)
        { if (obj == null) throw new ArgumentNullException(null); }
        protected static void ValidateForEmptiness(IEnumerable<T> values)
        { if (!values.Any()) throw new ArgumentException("empty"); }
        protected void ValidateIndex(int index)
        { if (index >= Size) throw new IndexOutOfRangeException("index"); }
    }
}