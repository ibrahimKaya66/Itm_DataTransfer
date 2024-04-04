
using DataTransfer.Business.Methods.Abstract;
using DataTransfer.Business.Services.Abstract;
using DataTransfer.Model.Entities;

namespace DataTransfer.Business.Methods.Concrete
{
    public class DepartmentMethod : IDepartmentMethod
    {
        private readonly IDepartmentService departmentService;

        public DepartmentMethod(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
        public List<Department>? Get()
        {
            try
            {
                var models = departmentService.GetAll().Where(m => m.IsDeleted == false).ToList();
                var responses = new List<Department>();
                foreach (var model in models)
                {
                    var response = mapper.Map<Department>(model);
                    responses.Add(response);
                }

                return responses;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Department?> Get(int id)
        {
            var model = await departmentService.GetAsync(id);
            if (model != null)
            {
                var response = mapper.Map<Department>(model);
                return response;
            }

            else
                return null;
        }

        public async Task<Department?> Post(Department model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            var entity = mapper.Map<Department>(model); // 'yu Department'a dönüştür
            entity.CreatedBy = "apiUser";
            entity.CreatedDate = now;
            try
            {
                await departmentService.AddAsync(entity);
                var response = mapper.Map<Department>(entity); // Department'ı 'ya dönüştür
                return response;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Department?> Put(int id, Department model)
        {
            DateTime utcNow = DateTime.UtcNow;
            var factory = factoryService.GetAll().FirstOrDefault();
            var utc = Convert.ToDouble(factory?.Country.UtcOffset ?? 3);
            DateTime now = utcNow.AddHours(utc);

            model.Id = id;
            var entity = await departmentService.GetAsync(id);
            if (entity == null)
            {
                return null;
            }
            mapper.Map(model, entity); // Department nesnesini entity'ye dönüştür
            entity.ModifiedBy = "apiUser";
            entity.ModifiedDate = now;
            try
            {
                await departmentService.UpdateAsync(entity);
                var response = mapper.Map<Department>(entity); // Güncellenmiş entity'yi Department'ya dönüştür

                return response;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<Department?> Delete(int id)
        {
            var model = await departmentService.GetAsync(id);
            if (model != null)
            {
                try
                {
                    await departmentService.RemoveAsync(model);
                    var response = mapper.Map<Department>(model);
                    return response;
                }
                catch (Exception)
                {
                    return null;
                }

            }
            else
                return null;
        }
    }
}
