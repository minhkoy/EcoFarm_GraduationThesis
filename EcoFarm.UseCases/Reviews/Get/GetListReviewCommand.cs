using EcoFarm.Application.Interfaces.Messagings;
using EcoFarm.UseCases.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EcoFarm.UseCases.Reviews.Get
{
    public class GetListReviewCommand : ICommand<List<ReviewDTO>>
    {
        public string PackageId { get; set; }

    }

}
