using DAL.DomainModel.Interfaces;

namespace DAL.DomainModel
{
    public class Rubric: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}