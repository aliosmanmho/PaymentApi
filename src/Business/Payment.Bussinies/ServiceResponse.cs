using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payment.Bussinies
{
    /// <summary>
    /// Generic Service Response
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceResponse<T> : IDisposable
    {
        public ServiceResponse()
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }
        public ServiceResponse(T data)
        {
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        /// <summary>
        /// Data Wrap
        /// </summary>
        public T Data { get; set; }
        /// <summary>
        /// Is Succeeded
        /// </summary>
        public bool Succeeded { get; set; }
        /// <summary>
        /// Errors
        /// </summary>
        public string[] Errors { get; set; }
        /// <summary>
        /// Detail Message
        /// </summary>
        public string Message { get; set; }

        ~ServiceResponse()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposeStatus)
        {
            if (disposeStatus == true)
            {

            }

        }

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
