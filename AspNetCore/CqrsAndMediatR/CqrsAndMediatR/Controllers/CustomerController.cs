﻿using AutoMapper;
using CqrsAndMediatR.Api.Models.Customer;
using CqrsAndMediatR.Domain.Entities;
using CqrsAndMediatR.Service.Command;
using CqrsAndMediatR.Service.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CqrsAndMediatR.Api.Controllers;

[ApiController]
[Route("Customer")]
public class CustomerController : Controller
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public CustomerController(IMediator mediator, IMapper mapper)
    {
        (_mediator, _mapper) = (mediator, mapper);
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("GetAll")]
    public async Task<ActionResult<List<GetAllCustomersResponse>>> GetAll()
    {
        try
        {
            var response = await _mediator.Send(new GetCustomersQuery());
            return _mapper.Map<List<GetAllCustomersResponse>>(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("CreateCustomer")]
    public async Task<ActionResult<CreateCustomerResponse>> Create(CreateCustomerRequest createCustomerModel)
    {
        try
        {
            var customer = _mapper.Map<Customer>(createCustomerModel);
            var response = await _mediator.Send(new CreateCustomerCommand
            {
                Customer = customer
            });

            return _mapper.Map<CreateCustomerResponse>(response);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("DeleteCustomer")]
    public async Task<ActionResult<DeleteCustomerResponse>> Delete(DeleteCustomerRequest deleteCustomerRequest)
    {
        try
        {
            var customer = _mapper.Map<Customer>(deleteCustomerRequest);
            var response = await _mediator.Send(new DeleteCustomerCommand
            {
                Customer = customer
            });

            return _mapper.Map<DeleteCustomerResponse>(response);
        }
        catch (Exception)
        {

            throw;
        }
    }


}
