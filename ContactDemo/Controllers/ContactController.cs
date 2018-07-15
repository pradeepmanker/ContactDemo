using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ContactDemo.Models;
using ContactDemo.Models.Validations;
using ContactDemo.Repository;
using PagedList;

namespace ContactDemo.Controllers
{
    public class ContactController : Controller
    {
        #region Previous Code
        //    private string url = System.Configuration.ConfigurationManager.AppSettings["APIServiceURL"];
        //    private HttpClient _client;

        //    public ContactController()
        //    {
        //        _client = new HttpClient();
        //        _client.BaseAddress = new Uri(url);
        //        _client.DefaultRequestHeaders.Accept.Clear();
        //        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    }

        //    public HttpClient Client
        //    {
        //        get
        //        {
        //            return _client;
        //        }
        //    }

        //    // GET: Contact
        //    public async Task<ActionResult> Index()
        //    {
        //        HttpResponseMessage responseMessage = await Client.GetAsync(url);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

        //            var contacts = JsonConvert.DeserializeObject<List<ContactMaster>>(responseData);

        //            return View(contacts);
        //        }
        //        return View("Error");
        //    }

        //    public ActionResult Create()
        //    {
        //        return View(new ContactMaster());
        //    }

        //    //The Post method
        //    [HttpPost]
        //    public async Task<ActionResult> Create(ContactMaster contact)
        //    {
        //        HttpResponseMessage responseMessage = await Client.PostAsJsonAsync(url, contact);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        var responseData = CreateJsonResponse<ApiResponse>(responseMessage).Result;
        //        AddResponseErrorsToModelState(responseData);

        //        return View("Create");
        //    }

        //    public async Task<ActionResult> Edit(Int64 id)
        //    {
        //        HttpResponseMessage responseMessage = await Client.GetAsync(url + "/" + id);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

        //            var contact = JsonConvert.DeserializeObject<ContactMaster>(responseData);

        //            return View(contact);
        //        }
        //        return View("Error");
        //    }

        //    //The PUT Method
        //    [HttpPost]
        //    public async Task<ActionResult> Edit(Int64 id, ContactMaster contact)
        //    {

        //        HttpResponseMessage responseMessage = await Client.PutAsJsonAsync(url + "/" + id, contact);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }

        //        var responseData = CreateJsonResponse<ApiResponse>(responseMessage).Result;
        //        AddResponseErrorsToModelState(responseData);

        //        return RedirectToAction("Edit");
        //    }

        //    public async Task<ActionResult> Delete(Int64 id)
        //    {
        //        HttpResponseMessage responseMessage = await Client.GetAsync(url + "/" + id);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

        //            var contact = JsonConvert.DeserializeObject<ContactMaster>(responseData);

        //            return View(contact);
        //        }
        //        return View("Error");
        //    }

        //    //The DELETE method
        //    [HttpPost]
        //    public async Task<ActionResult> Delete(Int64 id, ContactMaster contact)
        //    {

        //        HttpResponseMessage responseMessage = await Client.DeleteAsync(url + "/" + id);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Index");
        //        }
        //        return RedirectToAction("Delete");
        //    }

        //    public async Task<ActionResult> Details(int id)
        //    {
        //        HttpResponseMessage responseMessage = await Client.GetAsync(url + "/" + id);
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            var responseData = responseMessage.Content.ReadAsStringAsync().Result;

        //            var contact = JsonConvert.DeserializeObject<ContactMaster>(responseData);

        //            return View(contact);
        //        }
        //        return View("Error");
        //    }

        //    public ActionResult About()
        //    {
        //        ViewBag.Message = "Your application description page.";

        //        return View();
        //    }

        //    public ActionResult Error()
        //    {
        //        return View();
        //    }


        //    private static async Task<TResponse> CreateJsonResponse<TResponse>
        //(HttpResponseMessage response) where TResponse : ApiResponse, new()
        //    {
        //        var clientResponse = new TResponse
        //        {
        //            StatusIsSuccessful = response.IsSuccessStatusCode,
        //            ErrorState = response.IsSuccessStatusCode ? null :
        //                await DecodeContent<ErrorStateResponse>(response),
        //            ResponseCode = response.StatusCode
        //        };
        //        if (response.Content != null)
        //        {
        //            clientResponse.ResponseResult = await response.Content.ReadAsStringAsync();
        //        }

        //        return clientResponse;
        //    }

        //    private static async Task<TContentResponse>
        //        DecodeContent<TContentResponse>(HttpResponseMessage response)
        //    {
        //        var result = await response.Content.ReadAsStringAsync();
        //        return JsonConvert.DeserializeObject<TContentResponse>(result);
        //    }

        //    protected void AddResponseErrorsToModelState(ApiResponse response)
        //    {
        //        var errors = response.ErrorState.ModelState;
        //        if (errors == null)
        //            return;

        //        foreach (var error in errors)
        //        {
        //            this.ModelState.AddModelError(error.Key, error.Value[0]);
        //        }
        //    }

        #endregion

        private IContactUIManager _manager;
        public ContactController()
        {
            _manager = new ContactUIManager();
        }

        public ContactController(IContactUIManager manager)
        {
            _manager = manager;
        }

        // GET: Contact
        public ActionResult Index(string sortOrder, string CurrentSort, int? page, string searchString, string currentFilter)
        {
            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            ViewBag.CurrentSort = sortOrder;
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "FirstName" : sortOrder;
            IPagedList<ContactMaster> contacts = null;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var response = _manager.GetAllContacts();

            contacts = response.ToPagedList(pageIndex, pageSize);

            if (!String.IsNullOrEmpty(searchString))
            {
                contacts = contacts.Where(C => C.FirstName.Contains(searchString) || C.LastName.Contains(searchString)).ToPagedList(pageIndex, pageSize); ;
            }

            switch (sortOrder)
            {
                case "FirstName":
                    if (sortOrder.Equals(CurrentSort))
                        contacts = contacts.OrderByDescending(m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    else
                        contacts = contacts.OrderBy(m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
                case "LastName":
                    if (sortOrder.Equals(CurrentSort))
                        contacts = contacts.OrderByDescending(m => m.LastName).ToPagedList(pageIndex, pageSize);
                    else
                        contacts = contacts.OrderBy(m => m.LastName).ToPagedList(pageIndex, pageSize);
                    break;
                case "Default":
                    contacts = contacts.OrderBy(m => m.FirstName).ToPagedList(pageIndex, pageSize);
                    break;
            }
                        
            return View(contacts);
        }

        public ActionResult Create()
        {
            var response = _manager.CreateContact();
            return View(response);
        }

        //The Post method
        [HttpPost]
        public ActionResult Create(ContactMaster contact)
        {
            var response = _manager.CreateContact(contact);
            if (response.StatusIsSuccessful)
            {
                return RedirectToAction("Index");
            }
            AddResponseErrorsToModelState(response);

            return View("Create");
        }

        public ActionResult Edit(Int64 id)
        {
            var response = _manager.EditContact(id);
            return View(response);
        }

        //The PUT Method
        [HttpPost]
        public ActionResult Edit(Int64 id, ContactMaster contact)
        {
            var response = _manager.EditContact(id,contact);
            if (response.StatusIsSuccessful)
            {
                return RedirectToAction("Index");
            }
            AddResponseErrorsToModelState(response);

            return View("Edit");
        }

        public ActionResult Delete(Int64 id)
        {
            var response = _manager.Delete(id);
            return View(response);
        }

        //The DELETE method
        [HttpPost]
        public ActionResult Delete(Int64 id, ContactMaster contact)
        {
            var response = _manager.Delete(id, contact);
            if(response)
                return RedirectToAction("Index");
            return View("Delete");
        }

        public ActionResult Details(int id)
        {
            var response = _manager.GetContact(id);
            return View(response);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Error()
        {
            return View();
        }

        private void AddResponseErrorsToModelState(ApiResponse response)
        {
            var errors = response.ErrorState.ModelState;
            if (errors == null)
                return;

            foreach (var error in errors)
            {
                if (!this.ModelState.Where(MS => MS.Key == error.Key).Any())
                    this.ModelState.AddModelError(error.Key, error.Value[0]);
            }
        }
    }


}