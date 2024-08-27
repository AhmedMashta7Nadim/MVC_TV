using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Model_TV.Models;
using Models_TV.Model.Entity_programing;
using TV.Data;
using TV.Repositry.Serves;
 // ملاحظة مهمة : يوجد نوعين من الارجاع الاول 
 //T , وهذا النوع ارجاع من لكلاس نفسه 
 // اما ال , V
 //فهو ارجاع من الكلاس ساماري 
namespace TV.Repositry.RepoModels
{
    public class RepositryAllModel<T, V> : IRepositryAllModel<T, V>
       where T : Entity_Id
       where V : class
    {
        private readonly IMapper mapper;
        private readonly ApplicationDbContext context;

        public RepositryAllModel(
            IMapper mapper,
            ApplicationDbContext context
            )
        {
            this.mapper = mapper;
            this.context = context;
        }


        public virtual async Task<T> Add(T value)
        {
            var added = await context
                .Set<T>()
                .AddAsync(value);

            await context.SaveChangesAsync();

            return added.Entity;

        } //هذه الدالة من نوع فيرشوال , لانو عملتلا اوفر رايد في بعض الريبوستري

        public async Task<T> DeleteAsync(Guid id)
        {
            var get = await GetByIdPrivate(id);
            var delete = context.Set<T>().Remove(get);
            context.SaveChanges();
            return delete.Entity;
        } // الحذف النهائي

        public async Task<T> DetailsAsync(Guid? id)
        {
            if (id == null)
            {
                return null;
            }

            var get = await context.Set<T>()
                .AsNoTracking()
                //.FirstOrDefaultAsync(x => EF.Property<Guid>(x, "Id") == id);
                .FirstOrDefaultAsync(x => x.Id == id);

            if (get != null && typeof(T) == typeof(TV_Show))
            {
                context.Entry(get).Collection("languages").Load();
            }

            return get;
        }


        private int PageSize = 2; // خاصية بالبجنيشن 

        public async Task<List<V>> GetAllAsync()
        {
            

            var get = await context.Set<T>()
                                  .Where(x => x.IsActive)
                                  .ToListAsync();
            

            var maping = mapper.Map<List<V>>(get);

            return maping;
        }


        public async Task<V> GetById(Guid id)
        {
            var get = await context.Set<T>()
                .Where(x => x.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;

            var map = mapper.Map<V>(get);

            return map;

        }


        public async Task<List<T>> GetAllTAsync()
        {


            IQueryable<T> query = context.Set<T>()
                                  .Where(x => x.IsActive);


            
            if (typeof(T) == typeof(TV_Show))
            {
                query = query.Include(tvShow => (tvShow as TV_Show).languages);
            }
            if (typeof(T) == typeof(Languages))
            {
                query = query.Include(x => (x as Languages).tV_Shows);
            }


            var get = await query.ToListAsync();
            return get;
        }


        public async Task<T> GetByIdT(Guid id)
        {
            var get = await context.Set<T>().
                Where(x => x.IsActive)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            //IQueryable<T> query = context.Set<T>()
            //     .Where(x => x.IsActive)
            //     .AsNoTracking();
            //if (typeof(T)==typeof(TV_Show))
            //{
            //    query.Include("languages");
            //}
            //var get = await query.FirstOrDefaultAsync(x => x.Id == id);
            if (get == null)
            {
                return null;
            }
            return get;
        }

        public virtual async Task<T> Puts(Guid id, T value)
        {

            var get = await GetByIdPrivate(id);
            if (get == null)
            {
                return null;
            }

            var map = mapper.Map(value, get);
            try
            {
                var update = context.Set<T>()
                    .Update(map);
                context.SaveChanges();
            }
            catch (Exception exp)
            {
                throw new Exception();

            }


            return value;


        }


        public async Task<T> DeletedSoft(Guid id)
        {
            var get = await GetByIdPrivate(id);
            if (get == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }

            try
            {
                get.IsActive = false;
                //context.Entry(get).State = EntityState.Modified; هاد الكود بيعمل متل ال update تمام 
                context.Set<T>().Update(get);

                /*
                الفرق بين ال 
                update :
                تقوم بتحديث الكيان نفسه فقط في قاعدة البيانات
                بال patch 



                context.Entry(get).State = EntityState.Modified;
                بتحدث كل السطر 
                بستخدما بال put
                 */
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return get;
        } // الحذف اللطيف 

        private async Task<T> GetByIdPrivate(Guid id)
        {
            var get = await context.Set<T>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id) ?? null;


            return get;

        }

        public async Task<List<V>> GetAllAsyncP(int pageNumber)
        {
            var get = await context.Set<T>()
                                  .Where(x => x.IsActive)
                                  .Skip((pageNumber - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();



            var maping = mapper.Map<List<V>>(get);

            return maping;
        } // الارجاع الخاص بالبجنيشن 
    }

}
