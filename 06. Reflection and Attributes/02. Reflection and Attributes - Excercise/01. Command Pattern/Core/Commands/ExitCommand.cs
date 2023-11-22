using CommandPattern.Core.Contracts;
using System;
using System.Collections.Generic;
namespace CommandPattern.Core.Commands
{
    public class ExitCommand : ICommand
    {
        public string Execute(string[] args)
        {
            Environment.Exit(0);

            return null;
        }
    }
}
