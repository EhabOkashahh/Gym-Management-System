using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GymSystem.DAL.Entites.Enums;
using GymSystemBLL.Models.TrainerModels;

namespace GymSystemBLL.Models.SessionModels
{
    public interface IPopuateNaviagtionsProp
    {
        public IEnumerable<CategoryModelView>? Category { get; set; }
        public IEnumerable<TrainerModelView>? Trainers { get; set; }
    }
}