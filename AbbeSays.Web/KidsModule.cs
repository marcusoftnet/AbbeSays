using Nancy;
using Simple.Data;

namespace AbbeSays.Web
{
    public class KidsModule : NancyModule
    {
        private dynamic db;

        public KidsModule()
        {
            db = Database.Open();

            Get["/Kid/{id}"] = parameters => { return View["kid.cshtml", GetKid(parameters.Id)]; };
        }

        private dynamic GetKid(int id)
        {
            return db.Kids.Query()
                .Select(db.Kids.Id,
                        db.Kids.Name,
                        db.Kids.Bio,
                        db.Kids.BirthDate,
                        db.Kids.PictureURL,
                        db.Kids.Families.Name.As("ParentName")
                        )
                .Where(db.Kids.Id == id)
                .SingleOrDefault();
        }
    }
}