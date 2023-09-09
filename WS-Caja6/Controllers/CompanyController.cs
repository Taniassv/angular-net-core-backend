using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WS_Caja6.Models;
using WS_Caja6.Models.Request;
using WS_Caja6.Models.Response;

namespace WS_Caja6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompanyController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() 
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (DbCajaContext db = new DbCajaContext()) 
                { 
                    var list = db.Companies.OrderByDescending(x =>x.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = list;
                }
            } catch (Exception ex) 
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);

        }

        [HttpPost]
        public IActionResult Add(CompanyRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            try
            {
                using (DbCajaContext db = new DbCajaContext())
                {
                    Company oCompany = new Company();
                    oCompany.BusinessName = oModel.BusinessName;
                    oCompany.TaxId = oModel.TaxId;
                    oCompany.CompanyType = oModel.CompanyType;
                    oCompany.AccountNumber = oModel.AccountNumber;
                    oCompany.Cbu = oModel.Cbu;
                    db.Companies.Add(oCompany);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            } catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpPut]
        public IActionResult Edit(CompanyRequest oModel) 
        {
            Respuesta oRespuesta = new Respuesta();
            try 
            {
                using(DbCajaContext db = new DbCajaContext()) 
                {
                    Company oCompany = db.Companies.Find(oModel.Id);
                    oCompany.BusinessName = oModel.BusinessName;
                    oCompany.TaxId = oModel.TaxId;
                    oCompany.CompanyType = oModel.CompanyType;
                    oCompany.AccountNumber = oModel.AccountNumber;
                    oCompany.Cbu = oModel.Cbu;
                    db.Entry(oCompany).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch (Exception ex) 
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id) 
        {
            Respuesta oRespuesta = new Respuesta();
            try 
            {
                using(DbCajaContext db = new DbCajaContext()) 
                {
                    Company oCompany = db.Companies.Find(Id);
                    db.Remove(oCompany);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch(Exception ex) 
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
