using LetItGrow.Microservice.Common;
using System.Diagnostics.CodeAnalysis;

namespace LetItGrow.Microservice.Entities
{
    public static class Entity
    {
        /// <summary>
        /// Will check if the entity is null or not. When null it will throw a
        /// <see cref="ErrorException"/> of <see cref="Errors.NotFound"/>.
        /// </summary>
        /// <param name="entity">The entity to check.</param>
        /// <exception cref="ErrorException">With <see cref="Errors.NotFound"/> error.</exception>
        public static bool CheckFound(this IEntity? entity)
        {
            if (entity is null)
            {
                throw new ErrorException(Errors.NotFound);
            }
            return true;
        }

        /// <summary>
        /// Executes <see cref="CheckFound(IEntity?)"/> and then
        /// compares the concurrency stamps. If they are not equal
        /// a <see cref="ErrorException"/> is thrown of <see cref="Errors.Conflict"/>.
        /// </summary>
        /// <param name="obj">The object to check for.</param>
        /// <param name="rev">The rev to check against.</param>
        /// <exception cref="ErrorException">With <see cref="Errors.Conflict"/> error.</exception>
        public static bool CheckConcurrency(this IEntity? obj, string rev)
        {
            obj.CheckFound();
            if (obj!.Rev != rev)
            {
                throw new ErrorException(Errors.Conflict);
            }
            return true;
        }

        public static void SetIfNotEqual<T>(ref T? value, ref T? newValue)
            where T : class
        {
            if (value == newValue) return;

            value = newValue;
        }
    }
}