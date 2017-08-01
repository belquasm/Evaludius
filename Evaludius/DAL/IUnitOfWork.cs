// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using DAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IUnitOfWork
    {
        ICategoryRepository Categories { get; }
        ISkillSetRepository SkillSets { get; }
        ITeamRepository Teams { get; }
		IPlayerRepository Players { get; }
		ISkillSetItemRepository SkillSetItems { get; }

		int SaveChanges();
    }
}
