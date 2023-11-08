namespace EcoFarm.Domain.Common.Interfaces;

public interface IEntity
{
    string ID { get; set; }
    byte[] VERSION { get; set; } 
    DateTime CREATED_TIME { get; set; }
    DateTime? MODIFIED_TIME { get; set; }
    string CREATED_BY { get; set; }
    string MODIFIED_BY { get; set; }
    bool IS_ACTIVE { get; set; }
    bool IS_DELETE { get; set; }
}