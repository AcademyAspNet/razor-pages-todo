using RazorPagesTodoList.Models;
using System.Text.Json;

namespace RazorPagesTodoList.Repositories.Implementations
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        private readonly List<T> _entities;
        private int _nextId;

        protected Repository()
        {
            try
            {
                string json = File.ReadAllText(GetFilePath());
                List<T>? entities = JsonSerializer.Deserialize<List<T>>(json);

                if (entities == null)
                    throw new InvalidDataException("Failed to deserialize entities from json");

                _entities = entities;
            }
            catch (Exception)
            {
                _entities = new List<T>();
            }

            int maxId = 0;

            foreach (T entity in _entities)
            {
                if (entity.Id > maxId)
                    maxId = entity.Id;
            }

            _nextId = maxId + 1;
        }

        protected abstract string GetFilePath();

        public List<T> GetAll()
        {
            return _entities;
        }

        public T? GetById(int id)
        {
            foreach (T entity in _entities)
            {
                if (entity.Id == id)
                    return entity;
            }

            return null;
        }

        public void Create(T entity)
        {
            entity.Id = _nextId++;

            _entities.Add(entity);
            SaveChanges();
        }

        public void Delete(int id)
        {
            T entity;

            for (int i = 0; i < _entities.Count; i++)
            {
                entity = _entities[i];

                if (entity.Id == id)
                {
                    _entities.RemoveAt(i);
                    break;
                }
            }

            SaveChanges();
        }

        public void SaveChanges()
        {
            string json = JsonSerializer.Serialize(_entities);
            File.WriteAllText(GetFilePath(), json);
        }
    }
}
