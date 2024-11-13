using Application.Contracts;
using Application.DTOS;
using Application.DTOS.Responses;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repo
{
    public class ResponseRepo : IResponse
    {
        private readonly StudiaDBContext _dbContext;

        public ResponseRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseResponse> CreateResponseAsync(CreateResponseDTO responseDTO, int userId)
        {

            var response = new ResponseEntity
            {
                response = responseDTO.response,
                created_at = DateTime.UtcNow,
                id_user_id = userId
            };

            _dbContext.Responses.Add(response);
            await _dbContext.SaveChangesAsync();

            var result = "Response registered susscesfully";
            return new ResponseResponse 
            {
               result = result,
               responseId = response.id_response,
               id_user_id = response.id_user_id,
               response = response.response,
               CreatedAt = response.created_at

            };


        }
    }
}
