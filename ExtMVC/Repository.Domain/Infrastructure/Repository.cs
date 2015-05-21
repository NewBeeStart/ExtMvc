﻿/*----------------------------------------------------------------
// Copyright (C) 2014 郑州华粮科技股份有限公司
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

        public void Insert(TEntity entity)
        {
            dbSet.Add(entity);
           
        }

        public void InsertBatch(IEnumerable<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }

        public IList<TEntity> List()
        {
            return dbContext.Set<TEntity>().ToList();
        }

        public void Update(TEntity entity)
        {
            if (entity != null) 
            {
                dbSet.Attach(entity);
                dbContext.Entry(entity).State = EntityState.Modified;
            }
        }

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

        public void Delete(TEntity entity)
        {
            if (dbContext.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            dbSet.Remove(entity);
        }

        public void DeleteBatch(IEnumerable<TEntity> entities)
        {
            if (dbContext.Entry(entities).State == EntityState.Detached)
            {
                foreach (TEntity item in entities)
                {
                    dbSet.Attach(item);
                }
            }
            dbSet.RemoveRange(entities);
        }

        /// <summary>
        /// //var list = service.Query(p => p.UserId != Guid.Empty).OrderBy(p => p.UserName).Take(1).Skip(1).ToList();
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> filter)
        {
            if (filter==null)
            {
                return this.dbContext.Set<TEntity>();
            }
            return this.dbContext.Set<TEntity>().Where(filter); 
        }

        public IQueryable<TEntity> QueryByPage(Expression<Func<TEntity, bool>> FunWhere, Expression<Func<TEntity, string>> FunOrder,
                                                int PageSize, int PageIndex, out int recordsCount)
        {
            recordsCount = dbContext.Set<TEntity>().Where(FunWhere).OrderByDescending(FunOrder).Count();
            return
                dbContext.Set<TEntity>()
                         .Where(FunWhere)
                         .OrderByDescending(FunOrder)
                         .Select(t => t)
                         .Skip((PageIndex - 1)*PageSize)
                         .Take(PageSize);
        }

        /// <summary>
        /// 执行指定的sql语句
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
        /// 执行指定的sql语句
        /// </summary>
        /// <param name="sqlstr"></param>
        public int ExecuteStoreCommandUseTransaction(string sqlstr)
        {
            if (string.IsNullOrEmpty(sqlstr))
            {
                return 0;
            }
            using(DbTransaction trans=dbContext.Database.Connection.BeginTransaction())
            {

                trans.Commit();
            }

           return dbContext.Database.ExecuteSqlCommand(TransactionalBehavior.EnsureTransaction,sqlstr);
        }

        /// <summary>
        /// 查询指定的对象返回迭代器对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> SqlQuery(string sql,params object[] parameters)
        {
            if (string.IsNullOrEmpty(sql))
            {
                return null;
            }
            var result = dbContext.Database.SqlQuery<TEntity>(sql, parameters);
            return  result.ToList<TEntity>();
        }
    }

}
