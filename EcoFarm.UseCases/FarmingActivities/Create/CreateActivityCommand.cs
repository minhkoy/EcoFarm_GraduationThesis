using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingActivities.Create
{
    public class CreateActivityCommand : ICommand<bool>
    {
        public string PackageId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public List<string> Medias { get; set; }

        //public 
    }

    internal class CreateActivityHandler : ICommandHandler<CreateActivityCommand, bool>
    {
        public Task<Result<bool>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
