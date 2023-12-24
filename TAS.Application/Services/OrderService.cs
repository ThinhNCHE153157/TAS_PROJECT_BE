using Amazon.Runtime.Internal.Util;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAS.Application.Services.Interfaces;
using TAS.Data.Dtos.Responses;
using TAS.Data.EF;
using TAS.Data.EF.Repositories.Interfaces;
using TAS.Data.Entities;

namespace TAS.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrderService> _logger;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, ILogger<OrderService> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<Order> SaveOrder(PaymentResponseModel response)
        {
            Order order = new Order();
            VnPayHistory vnPayHistory = new VnPayHistory();
            try
            {
                if (response != null)
                {
                    if (response.VnPayResponseCode.Equals("00"))
                    {
                        order.OrderId = response.OrderId;
                        order.TotalAmount = response.Amount;
                        int lastSpaceIndex = response.OrderDescription.LastIndexOf(' ');
                        if (lastSpaceIndex != -1)
                        {
                            string modifiedString = response.OrderDescription.Substring(0, lastSpaceIndex);
                            string[] parts = modifiedString.Split(' ');
                            string lastPart = parts[parts.Length - 1];
                            string lastPart2 = parts[parts.Length - 2];
                            order.AccountId = Convert.ToInt32(lastPart2);
                            order.CourseId = Convert.ToInt32(lastPart);
                        }
                        var result = _unitOfWork.OrderRepository.saveOrder(order);
                        if (result==true)
                        {
                            vnPayHistory.TransactionId=response.TransactionId;
                            vnPayHistory.Amount=response.Amount;
                            vnPayHistory.OrderId=response.OrderId;
                            var result2 = _unitOfWork.OrderRepository.saveVNPayOrder(vnPayHistory);
                            if (result2==true)
                            {
                                return order;
                            }
                            else
                            {
                                return null;
                            }   

                        }
                    }
                }
                return null;
            }
            catch (Exception)
            {
                _logger.LogError("Error in SaveOrder");
                return null;
            }
        }
    }
}
