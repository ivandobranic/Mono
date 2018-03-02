namespace Project.Model.Common
{
    public interface IVehicleModel
    {
        int Id { get; set; }

        string Name { get; set; }

        int MakeId { get; set; }

        string Abrv { get; set; }
    }
}
