﻿using Entities.Models;
using System;
using System.Collections.Generic;

namespace Entities.ExtendedModels
{
    public class OwnerExtended
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public IEnumerable<Account> Accouts { get; set; }

        public OwnerExtended()
        {

        }

        public OwnerExtended(Owner owner)
        {
            Id = owner.Id;
            Name = owner.Name;
            DateOfBirth = owner.DateOfBirth;
            Address = owner.Address;
        }
    }
}