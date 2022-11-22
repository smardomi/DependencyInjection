using DependencyInjection.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependencyInjection.Controllers
{
    public class OperationsController : Controller
    {
        private readonly OperationService _operationService;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationScoped _scopedOperation;
        private readonly IOperationSingleton _singletonOperation;
        
        public OperationsController(OperationService operationService, IOperationTransient transientOperation, IOperationScoped scopedOperation, IOperationSingleton singletonOperation)
        {
            _operationService = operationService;
            _transientOperation = transientOperation;
            _scopedOperation = scopedOperation;
            _singletonOperation = singletonOperation;
        }

        public IActionResult Index()
        {
            OperationsProcess();
            return View();
        }

        public IActionResult GetOperations()
        {
            OperationsProcess();
            return PartialView("_OperationsPartial");
        }

        private void OperationsProcess()
        {
            // ViewBag contains controller-requested services
            ViewBag.Transient = _transientOperation.OperationId.ToString();
            ViewBag.Scoped = _scopedOperation.OperationId.ToString();
            ViewBag.Singleton = _singletonOperation.OperationId.ToString();

            // Operation service has its own requested services
            ViewBag.Transient2 = _operationService.TransientOperation.OperationId.ToString();
            ViewBag.Scoped2 = _operationService.ScopedOperation.OperationId.ToString();
            ViewBag.Singleton2 = _operationService.SingletonOperation.OperationId.ToString();
        }
    }
}