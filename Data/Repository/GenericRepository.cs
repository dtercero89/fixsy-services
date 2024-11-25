using System.ComponentModel;
using System.Data;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using FixsyWebApi.Data.Agg;
using FixsyWebApi.Data.UnitOfWork;

namespace FixsyWebApi.Data.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : IQueryableUnitOfWork
    {
        private readonly T _unitOfWork;
        private static readonly MethodInfo OrderByMethod = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderBy").Single(method => method.GetParameters().Length == 2);
        private static readonly MethodInfo OrderByDescending = typeof(Queryable).GetMethods().Where(method => method.Name == "OrderByDescending").Single(method => method.GetParameters().Length == 2);

        public GenericRepository(T unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private DbSet<TEntity> GetSet<TEntity>() where TEntity : EntityBase
        {
            return _unitOfWork.CreateSet<TEntity>();
        }

        /// <inheritdoc />
        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _unitOfWork;
            }
        }

        /// <inheritdoc />
        public void Add<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity != null)
            {
                entity.ModifiedBy = entity.ModifiedBy;
                entity.TransactionDate = DateTime.Now;
                entity.TransactionType = "Insert";
                GetSet<TEntity>().Add(entity); // add new item in this set
            }
            else
            {
                //TODO: Loguear error en base de datos;
            }
        }

        /// <inheritdoc />
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            if (entities != null)
            {
                GetSet<TEntity>().AddRange(entities);
            }
            else
            {
                //TODO: Loguear error en base de datos;
            }
        }

   
        /// <inheritdoc />
        public IEnumerable<TEntity> GetAll<TEntity>() where TEntity : EntityBase
        {
            return GetSet<TEntity>().ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>() where TEntity : EntityBase
        {
            return await GetSet<TEntity>().ToListAsync();
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> GetAll<TEntity>(List<string> includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(List<string> includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.ToListAsync();
        }

        /// <inheritdoc />
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return GetSet<TEntity>().FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return await GetSet<TEntity>().FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public TEntity GetSingle<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.FirstOrDefault(predicate);
        }

        /// <inheritdoc />
        public async Task<TEntity> GetSingleAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.FirstOrDefaultAsync(predicate);
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return GetSet<TEntity>().Where(predicate).ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : EntityBase
        {
            return await GetSet<TEntity>().Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public IEnumerable<TEntity> GetFiltered<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes) where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return items.Where(predicate).ToList();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<TEntity>> GetFilteredAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, List<string> includes)
            where TEntity : EntityBase
        {
            IQueryable<TEntity> items = GetSet<TEntity>();

            if (includes != null && includes.Any())
            {
                // Adding Includes to filter.
                items = includes.Aggregate(items, (current, include) => current.Include(include));
            }

            return await items.Where(predicate).ToListAsync();
        }

        /// <inheritdoc />
        public void Remove<TEntity>(TEntity entity) where TEntity : EntityBase
        {
            if (entity != null)
            {
                _unitOfWork.Attach(entity);
                GetSet<TEntity>().Remove(entity);
            }
            else
            {
                //TODO: Loguear error en base de datos;
            }
        }

        /// <inheritdoc />
        public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : EntityBase
        {
            if (entities != null)
            {
                GetSet<TEntity>().RemoveRange(entities);
            }
            else
            {
                //TODO: Loguear error en base de datos;
            }
        }

        public IEnumerable<TEntity> GetPagedAndFiltered<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters)
            where TEntity : EntityBase
        {
            var set = GetSet<TEntity>();

            if (filters != null && filters.Any())
            {
                Expression<Func<TEntity, bool>> filterExpression = null;

                foreach (KeyValuePair<string, object> filter in filters)
                {
                    if (filterExpression == null)
                    {
                        filterExpression = BuildContainsFuncFor<TEntity>(filter.Key, filter.Value);
                    }
                    else
                    {
                        filterExpression = AndAlso(filterExpression, BuildContainsFuncFor<TEntity>(filter.Key, filter.Value));
                    }

                }

                var filterCombinedExpression = filterExpression.Compile();

                return set
                  .Where(filterCombinedExpression)
                  .Skip(pageCount * pageIndex)
                  .Take(pageCount);
            }

            return set
              .Skip(pageCount * pageIndex)
              .Take(pageCount);

        }

        public async Task<List<TEntity>> GetPagedAndFilteredAsync<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters)
            where TEntity : EntityBase
        {
            var set = GetSet<TEntity>();

            // Construir la consulta base
            IQueryable<TEntity> query = set;

            // Aplicar filtros si existen
            if (filters != null && filters.Any())
            {
                foreach (var filter in filters)
                {
                    // Obtener la propiedad por el nombre
                    var parameter = Expression.Parameter(typeof(TEntity), "x");
                    var property = Expression.Property(parameter, filter.Key);
                    var value = Expression.Constant(filter.Value);

                    // Crear la expresión de comparación (Contains para cadenas)
                    var containsMethod = typeof(string).GetMethod("Equal", new[] { typeof(string) });
                    var filterExpression = Expression.Call(property, containsMethod, value);

                    // Agregar a la consulta
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(filterExpression, parameter);
                    query = query.Where(lambda);
                }
            }

            // Aplicar paginación y obtener los resultados
            return await query
                .Skip(pageCount * pageIndex)
                .Take(pageCount)
                .ToListAsync();
        }



        public IEnumerable<TEntity> GetPagedAndFiltered<TEntity>(int pageIndex, int pageCount, Dictionary<string, object> filters, string orderByField, bool ascending)
             where TEntity : EntityBase
        {
            var set = GetSet<TEntity>();

            if (filters != null && filters.Any())
            {
                Expression<Func<TEntity, bool>> filterExpression = null;

                foreach (KeyValuePair<string, object> filter in filters)
                {
                    if (filterExpression == null)
                    {
                        filterExpression = BuildContainsFuncFor<TEntity>(filter.Key, filter.Value);
                    }
                    else
                    {
                        filterExpression = AndAlso(filterExpression, BuildContainsFuncFor<TEntity>(filter.Key, filter.Value));
                    }

                }


                var filterCombinedExpression = filterExpression.Compile();

                if (!string.IsNullOrWhiteSpace(orderByField))
                {
                    var sortExpression = BuildSortFuncFor<TEntity>(orderByField);
                    if (ascending)
                    {
                        return set.OrderBy(sortExpression)
                          .Where(filterCombinedExpression)
                          .Skip(pageCount * pageIndex)
                          .Take(pageCount);
                    }
                    return set.OrderByDescending(sortExpression)
                        .Where(filterCombinedExpression)
                        .Skip(pageCount * pageIndex)
                        .Take(pageCount);
                }

                return set
                  .Where(filterCombinedExpression)
                  .Skip(pageCount * pageIndex)
                  .Take(pageCount);
            }


            if (!string.IsNullOrWhiteSpace(orderByField))
            {
                var sortExpression = BuildSortFuncFor<TEntity>(orderByField);
                if (ascending)
                {
                    return set.OrderBy(sortExpression)
                      .Skip(pageCount * pageIndex)
                      .Take(pageCount);
                }
                return set.OrderByDescending(sortExpression)
                    .Skip(pageCount * pageIndex)
                    .Take(pageCount);
            }


            return set
              .Skip(pageCount * pageIndex)
              .Take(pageCount);

        }

        private static Func<TEntity, dynamic> BuildSortFuncFor<TEntity>(string sortByKey)
        {
            var param = Expression.Parameter(typeof(TEntity));
            return Expression.Lambda<Func<TEntity, dynamic>>(Expression.TypeAs(Expression.Property(param, sortByKey), typeof(object)), param).Compile();
        }

        private static Expression<Func<T, bool>> AndAlso<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2) where T : EntityBase
        {
            ParameterExpression param = expr1.Parameters[0];
            if (ReferenceEquals(param, expr2.Parameters[0]))
            {
                return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, expr2.Body), param);
            }
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(expr1.Body, Expression.Invoke(expr2, param)), param);
        }


        private static Expression<Func<TEntity, bool>> BuildContainsFuncFor<TEntity>(string propertyName, object propertyValue)
            where TEntity : EntityBase
        {
            var parameterExp = Expression.Parameter(typeof(TEntity), "type");
            var propertyExp = Expression.Property(parameterExp, propertyName);

            if (propertyExp.Type != typeof(string))
            {
                return GetExpression<TEntity>(propertyName, "Equal", propertyValue.ToString());
                //return GetExpression<T>(propertyName, "Equal", propertyValue.ToString()).Compile();
            }


            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });

            if (propertyValue == null)
            {
                propertyValue = string.Empty;
            }

            var someValue = Expression.Constant(propertyValue.ToString(), typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            var expression = Expression.Lambda<Func<TEntity, bool>>(containsMethodExp, parameterExp);
            return expression;
        }


        private static Expression<Func<TEntity, bool>> GetExpression<TEntity>(string propertyName, string operatorType, string propertyValue) where TEntity : EntityBase
        {
            var isNegated = operatorType.StartsWith("!");
            if (isNegated)
                operatorType = operatorType.Substring(1);

            var parameter = Expression.Parameter(typeof(T), "type");
            var property = Expression.Property(parameter, propertyName);

            var td = TypeDescriptor.GetConverter(property.Type);

            var constantValue = Expression.Constant(td.ConvertFromString(propertyValue), property.Type);

            // Check if specified method is an Expression member
            var operatorMethod = typeof(Expression).GetMethod(operatorType, new[] { typeof(MemberExpression), typeof(ConstantExpression) });

            Expression expression;

            if (operatorMethod == null)
            {
                // Execute against type members
                var method = property.Type.GetMethod(operatorType, new[] { property.Type });
                expression = Expression.Call(property, method, constantValue);
            }
            else
            {
                // Execute the passed operator method (e.g. Expression.GreaterThan)
                expression = (Expression)operatorMethod.Invoke(null, new object[] { property, constantValue });
            }

            if (isNegated)
                expression = Expression.Not(expression);

            return Expression.Lambda<Func<TEntity, bool>>(expression, parameter);
        }

        public void Modify<TEntity>(TEntity item) where TEntity : EntityBase
        {
            if (item != null)
                _unitOfWork.SetModified(item);
            else
            {
                //TODO: Loguear error en base de datos;
            }
        }

         public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, Dictionary<string, object> parameters)
        {
            SqlParameter[] parameters2 = CreateSqlParameters(parameters);
            return _unitOfWork.ExecuteQuery<TEntity>(sqlQuery, parameters2);
        }

        public IEnumerable<TEntity> ExecuteQuery<TEntity>(string sqlQuery, IEnumerable<object> parameters)
        {
            return _unitOfWork.ExecuteQuery<TEntity>(sqlQuery, parameters.ToArray());
        }

        private SqlParameter[] CreateSqlParameters(Dictionary<string, object> parameters)
        {
            if (parameters != null && parameters.Any())
            {
                SqlParameter[] array = new SqlParameter[parameters.Count];
                int num = 0;
                foreach (KeyValuePair<string, object> parameter in parameters)
                {
                    if (parameter.Value is CustomTypeQueryDefinition)
                    {
                        CustomTypeQueryDefinition customTypeQueryDefinition = parameter.Value as CustomTypeQueryDefinition;
                        array[num] = GetSqlParameterCustomType(parameter.Key, customTypeQueryDefinition);
                    }
                    else
                    {
                        array[num] = new SqlParameter(parameter.Key, parameter.Value);
                    }

                    num++;
                }

                return array;
            }

            return new SqlParameter[0];
        }

        private SqlParameter GetSqlParameterCustomType(string key, CustomTypeQueryDefinition customTypeQueryDefinition)
        {
            SqlParameter sqlParameter = new SqlParameter(key, SqlDbType.Structured);
            sqlParameter.Direction = ParameterDirection.Input;
            sqlParameter.TypeName = customTypeQueryDefinition.SqlTypeName;
            sqlParameter.Value = CrearDataTable(customTypeQueryDefinition.Values);
            return sqlParameter;
        }


        private static DataTable CrearDataTable(IEnumerable<string> id)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(string));
            foreach (string item in id)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["Id"] = item;
                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

        public void Dispose()
        {
            if (_unitOfWork != null)
                _unitOfWork.Dispose();
        }

    }
}
