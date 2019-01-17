using Dapper;
using OnlineTalent.Core.Common;
using OnlineTalent.Core.EnumTypes;
using OnlineTalent.Core.Models;
using OnlineTalent.Log;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace OnlineTalent.Database
{
    public class Repository<T>:IRepository<T> where T : class
    {
        public readonly IDbConnection _dbConnection;
        private IConnectionFactory _connectionFactory;

        public Repository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _dbConnection = _connectionFactory.GetOpenConnection();
        }
        public T FindById(int id, ref ReturnOutput returnOutput)
        {
            string methodName = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

            IEnumerable<T> list = null;
            //returnOutput.ErrorCode = EnumTypes.ErrorCode.Success.ToString();
            //returnOutput.ErrorMessage = EnumTypes.ErrorMessages.Ok.ToString();

            try
            {
                using (var conn = _dbConnection)
                {
                    list = conn.GetList<T>(new { Id = id });
                }
            
            }
            catch (Exception ex)
            {
                string errorResult = ex.Message;
            
                returnOutput.ErrorCode = EnumTypes.ErrorCode.Error.ToString();
                returnOutput.ErrorMessage = errorResult;

                ReceiverData receiverData = new ReceiverData();
                receiverData.Log(methodName, errorResult);
            }
            finally
            {
                _connectionFactory.GetCloseConnection(_dbConnection);
            }

            if (list is null && list.Count() == 0)
                return null;

            return list.FirstOrDefault();
        }

        public IEnumerable<T> Find(string whereQuery, ref ReturnOutput returnOutput)
        {
            string methodName = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

            IEnumerable<T> list = null;

            //returnOutput.ErrorCode = EnumTypes.ErrorCode.Success.ToString();
            //returnOutput.ErrorMessage = EnumTypes.ErrorMessages.Ok.ToString();

            try
            {
                using (var conn = _dbConnection)
                {
                    list = conn.GetList<T>(whereQuery);

                }
            }
            catch (Exception ex)
            {
                string errorResult = ex.Message;
              //  HelperText.CreateTextFile("Find", result);

                returnOutput.ErrorCode = EnumTypes.ErrorCode.Error.ToString();
                returnOutput.ErrorMessage = errorResult;

                ReceiverData receiverData = new ReceiverData();
                receiverData.Log(methodName, errorResult);
            }
            finally
            {
                _connectionFactory.GetCloseConnection(_dbConnection);
            }

            return list;
        }

        public int Add(T entity, ref ReturnOutput returnOutput)
        {
            string methodName = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

            //returnOutput.ErrorCode = EnumTypes.ErrorCode.Success.ToString();
            //returnOutput.ErrorMessage = EnumTypes.ErrorMessages.Ok.ToString();

            int? id = 0;
            try
            {
                using (var conn = _dbConnection)
                {
                    id = conn.Insert(entity);
                }
            }
            catch (Exception ex)
            {
                string errorResult = ex.Message;
        
                returnOutput.ErrorCode = EnumTypes.ErrorCode.Error.ToString();
                returnOutput.ErrorMessage = errorResult;

                ReceiverData receiverData = new ReceiverData();
                receiverData.Log(methodName, errorResult);
            }
            finally
            {
                _connectionFactory.GetCloseConnection(_dbConnection);
            }

            return id.Value;
        }
        public T Update(T entity, ref ReturnOutput returnOutput)
        {
            string methodName = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

            //returnOutput.ErrorCode = EnumTypes.ErrorCode.Success.ToString();
            //returnOutput.ErrorMessage = EnumTypes.ErrorMessages.Ok.ToString();

            int updatedRows = 0;
            try
            {
                using (var conn = _dbConnection)
                {
                    updatedRows = conn.Update(entity);
                }
            }
            catch (Exception ex)
            {
                string errorResult = ex.Message;

                returnOutput.ErrorCode = EnumTypes.ErrorCode.Error.ToString();
                returnOutput.ErrorMessage = errorResult;

                ReceiverData receiverData = new ReceiverData();
                receiverData.Log(methodName, errorResult);
            }
            finally
            {
                _connectionFactory.GetCloseConnection(_dbConnection);
            }

            return entity;
        }
        public void Delete(T entity, ref ReturnOutput returnOutput)
        {
            string methodName = string.Format("{0}.{1}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name);

            //returnOutput.ErrorCode = EnumTypes.ErrorCode.Success.ToString();
            //returnOutput.ErrorMessage = EnumTypes.ErrorMessages.Ok.ToString();

            int? id = 0;
            try
            {
                using (var conn = _dbConnection)
                {
                    id = conn.Delete(entity);
                }
            }
            catch (Exception ex)
            {
                string errorResult = ex.Message;
               
                returnOutput.ErrorCode = EnumTypes.ErrorCode.Error.ToString();
                returnOutput.ErrorMessage = errorResult;

                ReceiverData receiverData = new ReceiverData();
                receiverData.Log(methodName, errorResult);
            }
            finally
            {
                _connectionFactory.GetCloseConnection(_dbConnection);
            }
        }

    }
}
