using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.FarmingPackages.AddNewMedia
{
    public class PackageAddNewMediaCommand : ICommand<bool>
    {
        public string PackageId { get; set; }
        public List<Blob> PackageMedias { get; set; }
    }

    internal class Handler : ICommandHandler<PackageAddNewMediaCommand, bool>
    {
        public Task<Result<bool>> Handle(PackageAddNewMediaCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
