using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Common.Service
{
	public interface IEvent
	{
        /// <summary>
		/// Organization Id
		/// </summary>
		Guid Oid { get; set; }

        /// <summary>
		/// Saga Id
		/// </summary>
		Guid Sid { get; set; }

        /// <summary>
        /// Event Type Full Namespace
        /// </summary>
        public string Type { get; set; }
    }
}
