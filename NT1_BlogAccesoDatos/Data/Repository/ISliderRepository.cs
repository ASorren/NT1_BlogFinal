using Microsoft.AspNetCore.Mvc.Rendering;
using NT1_Blog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NT1_BlogAccesoDatos.Data.Repository
{
    public interface ISliderRepository : IRepository<Slider> 
    {


        void Update(Slider slider);
    }
}
