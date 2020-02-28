using System;

namespace RestEasy.Core.Markers
{
    public interface IDomain<T> where T : IDto
    {
        Guid Id { get; }

        void Map(T dto);

        T Map();
    }
}