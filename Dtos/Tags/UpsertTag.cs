using System.ComponentModel.DataAnnotations;
using Dtos.Core;

namespace Dtos.Tags;

public class UpsertTag : BaseArgs
{
    public string Name { get; set; }
    public List<Computer> Computers { get; set; }
}

public class Computer
{
    public string Name { get; set; }
    public long ManufactureDate { get; set; }

    public ComputerType Type { get; set; }
}

public enum ComputerType
{
    All = 0,
    PC = 1,
    Laptop = 2,
    Handheld = 3,
}