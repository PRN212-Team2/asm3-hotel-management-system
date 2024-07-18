using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification() { }

        /// <summary>
        /// Passes the criteria to construct a specification with specific conditions.
        /// </summary>
        /// <param name="criteria"></param>
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// The conditions or criteria to get an entity or entities.
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// The list of Includes expressions that an entity includes the related entities 
        /// through navigation properties.
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Used for sorting ascendingly
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; private set; }

        /// <summary>
        /// Used for sorting descendingly
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        /// <summary>
        /// The list of custom include queries for nested include entities
        /// </summary>
        public List<Func<IQueryable<T>, IIncludableQueryable<T, object>>> CustomIncludes { get; } = new List<Func<IQueryable<T>, IIncludableQueryable<T, object>>>();

        /// <summary>
        /// Adds Include expression to the Includes list
        /// </summary>
        /// <param name="includeExpression"></param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }


        /// <summary>
        /// Add custom include queries to the CustomIncludes list
        /// </summary>
        /// <param name="customIncludeExpression"></param>
        protected void AddCustomInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> customIncludeExpression)
        {
            CustomIncludes.Add(customIncludeExpression);
        }

        /// <summary>
        /// Assigns the sorting ascendingly expression.
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        /// <summary>
        /// Assigns the sorting descendingly expression.
        /// </summary>
        /// <param name="orderByDescExpression"></param>
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
