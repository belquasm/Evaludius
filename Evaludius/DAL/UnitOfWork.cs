// ======================================
// Author: Ebenezer Monney
// Email:  info@ebenmonney.com
// Copyright (c) 2017 www.ebenmonney.com
// 
// ==> Gun4Hire: contact@ebenmonney.com
// ======================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ApplicationDbContext _context;

        ICategoryRepository _categories;
        ISkillSetRepository _skillSets;
        ITeamRepository _teams;
		IPlayerRepository _players;		

		ISkillSetItemRepository _skillSetItems;

		public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }



        public ICategoryRepository Categories
        {
            get
            {
                if (_categories == null)
                    _categories = new CategoryRepository(_context);

                return _categories;
            }
        }



        public ISkillSetRepository SkillSets
        {
            get
            {
                if (_skillSets == null)
                    _skillSets = new SkillSetRepository(_context);

                return _skillSets;
            }
        }

		public ISkillSetItemRepository SkillSetItems
		{
			get
			{
				if (_skillSetItems == null)
					_skillSetItems = new SkillSetItemRepository(_context);

				return _skillSetItems;
			}
		}

		public ITeamRepository Teams
        {
            get
            {
                if (_teams == null)
                    _teams = new TeamRepository(_context);

                return _teams;
            }
        }

		public IPlayerRepository Players
		{
			get
			{
				if (_players == null)
					_players = new PlayerRepository(_context);

				return _players;
			}
		}

	

		public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
