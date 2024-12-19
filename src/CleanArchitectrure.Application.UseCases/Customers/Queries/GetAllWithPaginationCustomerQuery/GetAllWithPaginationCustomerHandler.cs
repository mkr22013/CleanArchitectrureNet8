using AutoMapper;
using CleanArchitectrure.Application.Dto;
using CleanArchitectrure.Application.Interface.Persistence;
using CleanArchitectrure.Application.UseCases.Commons.Bases;
using CleanArchitectrure.Application.UseCases.Commons.Util;
using MediatR;

namespace CleanArchitectrure.Application.UseCases.Customers.Queries.GetAllWithPaginationCustomerQuery
{
    /// <summary>
    /// 
    /// </summary>
    internal class GetAllWithPaginationCustomerHandler : IRequestHandler<GetAllWithPaginationCustomerQuery, BaseResponsePagination<IEnumerable<CustomerDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        /// <summary>
        /// GetAllWithPaginationCustomerHandler
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        /// <exception cref="ArgumentNullException"></exception>
        public GetAllWithPaginationCustomerHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Handle
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<BaseResponsePagination<IEnumerable<CustomerDto>>> Handle(GetAllWithPaginationCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponsePagination<IEnumerable<CustomerDto>>();
            try
            {
                var count = await _unitOfWork.QCustomers.CountAsync();

                var customers = await _unitOfWork.QCustomers.GetAllWithPaginationAsync(request.PageNumber, request.PageSize);

                if (customers is not null)
                {
                    response.PageNumber = request.PageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)request.PageSize);
                    response.TotalCount = count;
                    response.Data = _mapper.Map<IEnumerable<CustomerDto>>(customers);
                    response.Succcess = true;
                    response.Message = "Query succeed!";
                }
            }
            catch (Exception ex)
            {
                var lstErrors = new List<BaseError>();
                var error = new BaseError() { ErrorMessage = Convert.ToString(ex.Message) };
                lstErrors.Add(error);
                response.Errors = lstErrors;
                response.Message = ex.Message;               
            }
            return response;
        }
    }
}
