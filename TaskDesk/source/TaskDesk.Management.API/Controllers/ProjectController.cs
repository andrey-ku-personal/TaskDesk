using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using TaskDesk.Management.Handlers.Project;
using TaskDesk.Shared.Endpoints;
using TaskDesk.Shared.Queries.Filter;
using TaskDesk.SharedModel.Project.Models;

namespace TaskDesk.Management.API.Controllers;

public class ProjectController : BaseEndpoint
{
    public ProjectController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("Project/Create")]
    [OpenApiOperation(
        operationId: "Project.Create",
        summary: "Create Project",
        description: "Create Project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Create([FromBody] CreateRequest request) => await Send<CreateRequest, ProjectModel>(request);

    [HttpPost("Project/Get")]
    [OpenApiOperation(
       operationId: "Project.Get",
       summary: "Get Project",
       description: "Get Project")
    ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> Get([FromBody] GetRequest request) => await Send<GetRequest, ProjectModel>(request);

    [HttpPost("Project/GetAll")]
    [OpenApiOperation(
      operationId: "Project.GetAll",
      summary: "GetAll Projects",
      description: "GetAll Projects")
   ]
    [OpenApiTags("Project")]
    public async Task<ActionResult> GetAll([FromBody] GetAllRequest request) => await Send<GetAllRequest, FilteredResult<ProjectViewModel>>(request);
}
