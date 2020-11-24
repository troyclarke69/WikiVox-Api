﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Wikivox_Api.Models
{
	public abstract class QueryStringParameters
	{
		const int maxPageSize = 50;
		public int PageNumber { get; set; } = 1;
		private int _pageSize = 50;
		public int PageSize
		{
			get
			{
				return _pageSize;
			}
			set
			{
				_pageSize = (value > maxPageSize) ? maxPageSize : value;
			}
		}
	}
}
