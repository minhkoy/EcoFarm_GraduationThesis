using Ardalis.Result;
using EcoFarm.Application.Interfaces.Messagings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm.UseCases.Accounts.Delete
{
    public class DeleteAccountCommand : ICommand<bool>
    {
        public string Id { get; set; }
        public DeleteAccountCommand(string id)
        {
            Id = id;
        }
        public DeleteAccountCommand()
        {

        }
    }

    internal class Hander : ICommandHandler<DeleteAccountCommand, bool>
    {
        public Task<Result<bool>> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
