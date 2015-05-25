/*----------------------------------------------------------------
// Copyright (C) 2014 NewBee工作室
// 版权所有。 
//
// 文件名：Repository
// 文件功能描述：
//
// 创建标识：xycui 2014/8/26 14:31:17
//
// 修改标识：
// 修改描述：
//----------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Repository.Domain.Infrastructure;
using System.Data.Common;
using System.Transactions;

namespace Repository.Domain.Infrastructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected TestDbContext dbContext;
        protected DbSet<TEntity> dbSet;

        /// <summary>
        /// 无参构造函数
        /// </summary>
        public Repository()
        {
        }

        public Repository(TestDbContext Context)
        {
            this.dbContext = Context;
            this.dbSet = dbContext.Set<TEntity>();
        }

        public void SetDBContext(TestDbContext Context)
        {
            this.dbContext = Context;
            this.dbSet = dbContext.Set<TEntity>();
        }

        /// <summary>
        /// 获取数据列表
        /// </summary>
        /// <returns></returns>
        public IList<TEntity> List()
        {
            return dbContext.Set<TEntity>().ToList();
        }
        #region Insert
        /// <summary>
        /// 新增单条数据
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);

        }
        /// <summary>
        /// 批量新增数据
        /// </summary>
        /// <param name="entities"></param>
        public void InsertBatch(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }
        #endregion
        #region Update
        /// <summary>
        /// 更新单挑数据
        /// </summary>
        /// <param name="entity"></param>
        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
        }
        /// <summary>
        /// 批量更新数据
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateBatch(IEnumerable<TEntity> entities)
        {
            if (entities != null && entities.Count() > 0)
            {
                foreach (TEntity item in entities)
                {
                    dbSet.Attach(item);
                    dbContext.Entry(item).State = EntityState.Modified;
                }
            }
        }
        #endregion
        #region Delete
        /// <summary>
        /// 删除单条数据
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        /// <summary>
        /// 通过条件批量删除
        /// </summary>
        /// <param name="filter"></param>
        public void DeleteByWhere(Expression<Func<TEntity, bool>> filter)
        {
            var temp = this.Query(filter).ToList();
            DeleteBatch(temp);
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteBatch(IEnumerable<TEntity> entities)
        {
            foreach (TEntity item in entities)
            {
                if (dbContext.Entry(item).State == EntityState.Detached)
                {
                    dbSet.Attach(item);
                }
            }
            dbSet.RemoveRange(entities);
        }

        #endregion
        #region Select
        /// <summary>
        /// 查询数据
        ///   var list = service.Query(p => p.UserId != Guid.Empty).OrderBy(p => p.UserName).Take(1).Skip(1).ToList();
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            if (filter == null)
            {
                return this.dbContext.Set<TEntity>();
            }
            return this.dbContext.Set<TEntity>().Where(filter);
        }


        /// <summary>
        /// 分页查询数据
        /// </summary>
        /// <param name="FunWhere"></param>
        /// <param name="FunOrder"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="recordsCount"></param>
        /// <returns></returns>
        public IQueryable<TEntity> QueryByPage(Expression<Func<TEntity, bool>> FunWhere, Expression<Func<TEntity, string>> FunOrder,
                                                int PageSize, int PageIndex, out int recordsCount)
        {
            recordsCount = dbContext.Set<TEntity>().Where(FunWhere).OrderByDescending(FunOrder).Count();
            return
                dbContext.Set<TEntity>()
                         .Where(FunWhere)
                         .OrderByDescending(FunOrder)
                         .Select(t => t)
                         .Skip((PageIndex - 1) * PageSize)
                         .Take(PageSize);
        }

        /// <summary>
        /// 查询指定的对象返回迭代器对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery<TEntity>(string sql, params object[] parameters)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }
            var result = dbContext.Database.SqlQuery<TEntity>(sql, parameters);
            return result.ToList<TEntity>();
        }
        #endregion
        #region ExecuteCmd
        /// <summary>
        /// 执行单条sql语句
        /// </summary>
        /// <param name="sqlstr"></param>
        public int ExecuteStoreCommand(string sqlstr)
        {
            if (string.IsNullOrEmpty(sqlstr))
            {
                return 0;
            }

            return dbContext.Database.ExecuteSqlCommand(sqlstr);
        }

        /// <summary>
        /// 批量事务执行多条sql
        /// </summary>
        /// <param name="sqlstr"></param>
        public void ExecuteStoreCommands(List<string> sqllst)
        {
            if (null == sqllst)
            {
                throw new Exception("the paramater of sqllst is null!");
            }
            if (sqllst.Count() > 0)
            {
                using (TransactionScope transaction = new TransactionScope())
                {
                    foreach (string sql in sqllst)
                    {
                        dbContext.Database.ExecuteSqlCommand(sql);
                        dbContext.SaveChanges();
                    }
                    transaction.Complete();
                }
            }
        }
        #endregion

    }

}
