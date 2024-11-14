using Application.Contracts;
using Application.DTOS.Responses;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repo
{
    public class MaterialRepo : IMaterials
    {
        private readonly StudiaDBContext _dbContext;

        public MaterialRepo(StudiaDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MaterialResponse>> GetMaterialsByUserIdAsync(int userId)
        {
            return await _dbContext.Materials
                .Where(m => m.id_user_id == userId)
                .Include(m => m.Exam)
                .Include(m => m.Flashcard)
                .Include(m => m.Resume)
                .Select(m => new MaterialResponse
                {
                    materialId = m.id_material,
                    id_user_id = m.id_user_id,
                    id_exam_id = m.id_exam_id ?? 0,
                    id_flashcard_id = m.id_flashcard_id ?? 0,
                    id_resume_id = m.id_resume_id ?? 0,
                })
                .ToListAsync();
        }
    }
}
