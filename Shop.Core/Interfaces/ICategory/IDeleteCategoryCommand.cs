﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Interfaces.ICategory;
public interface IDeleteCategoryCommand
{
    Task<int> ExecuteAsync(string name);
}