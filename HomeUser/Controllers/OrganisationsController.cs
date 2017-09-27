using CoreEntities.ViewModels;
using Microsoft.Owin.Security;
using RepositoryLayer;
using ServiceLayer.Interfaces;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HomeUser.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("api/Organisations")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrganisationsController : ApiController
    {


        private vCIOPRoEntities _ctx = new vCIOPRoEntities();
        private IOrganisations _tenantDataManager;

        public OrganisationsController(IOrganisations tntMgr)
        {
            _tenantDataManager = tntMgr;

        }
        [HttpGet]
        [Route("api/Organisations/GetAllOrg")]
        public IHttpActionResult GetAllOrg()
        {
            var organisations = _tenantDataManager.GetAllOrganisations();

            return Ok(organisations);
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("/api/Organisations/GetAllOrgById/{id:int}")]
        public IHttpActionResult GetAllOrgById(int id)
        {
            var result = _tenantDataManager.GetOrgById(id);

            return Ok(result);
        }
        public ISecureDataFormat<AuthenticationTicket> AccessTokenFormat { get; private set; }
        [HttpPost]
        [Route("api/Organisations/AddOrg")]
        public IHttpActionResult AddOrg(OrganisationsViewModel model)
        {
            ResponseViewModel ResponseObj = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                

                bool orgAdded = _tenantDataManager.AddOrg(model);
                return Ok("Ok");
            }

            catch (Exception EX)
            {
                throw EX;
            }

            return BadRequest(ModelState);
        }
        //put
        [Route("api/Organisations/EditOrg")]
        public IHttpActionResult EditOrg(OrganisationsViewModel model)
        {
            ResponseViewModel ResponseObj = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // if (result.Succeeded == true)
                //

                bool orgedited = _tenantDataManager.EditOrg(model);
                return Ok("Ok");
                // }
            }
            catch (Exception EX)
            {
                throw EX;
            }
            return BadRequest(ModelState);

        }
        [Route("api/Organisations/DeleteOrg")]
        public IHttpActionResult DeleteOrg(int OrganisationId)
        {
            ResponseViewModel ResponseObj = new ResponseViewModel();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // if (result.Succeeded == true)
                //

                bool orgdeleted = _tenantDataManager.DeleteOrg(OrganisationId);
                // }
            }
            catch (Exception EX)
            {
                throw EX;
            }

            return Ok();
        }
    }
}