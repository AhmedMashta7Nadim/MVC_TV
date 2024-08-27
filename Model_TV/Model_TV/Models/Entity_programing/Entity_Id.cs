using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models_TV.Model.Entity_programing
{
    public class Entity_Id
    {
        public virtual Guid Id { get; set; }

        public bool IsActive { get; set; } = true;



        public Entity_Id()
        {
            Id=Guid.NewGuid();
           
        }
    }
}
