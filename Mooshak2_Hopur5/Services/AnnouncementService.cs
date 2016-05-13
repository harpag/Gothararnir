using Mooshak2_Hopur5.Models.Entities;
using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mooshak2_Hopur5.Services
{
    public class AnnouncementService
    {
        private DataModel _db;

        public AnnouncementService()
        {
            _db = new DataModel();
        }

        public AnnouncementViewModel getAnnouncementById(int announcementId)
        {
            //Sækir tilkynningu með ákveðnu ID ofan í gagnagrunn
            var announcement = _db.Announcement.SingleOrDefault(x => x.announcementId == announcementId);
            //Kastar villu ef ekki fannst tilkynning með þessu ID

            if (announcement == null)
            {
                throw new Exception();
            }

            //Tilkynning sett inn í ViewModelið
            var viewModel = new AnnouncementViewModel
            {
                DateCreate = announcement.dateCreate,
                Announcement = announcement.announcement
            };
            return viewModel;
        }

        public AnnouncementViewModel getAllAnnouncements()
        {
            var announcements = (from a in _db.Announcement
                                 orderby a.dateCreate descending
                                 select a).ToList();

            List<AnnouncementViewModel> announcementList;
            announcementList = new List<AnnouncementViewModel>();

            //Tilkynningar settar í lista
            foreach (var entity in announcements)
            {
                var result = new AnnouncementViewModel
                {
                    Announcement = entity.announcement,
                    DateCreate = entity.dateCreate
                };
                announcementList.Add(result);
            }

            AnnouncementViewModel viewModel = new AnnouncementViewModel
            {
                AnnouncementList = announcementList
            };
            return viewModel;
        }

        public Boolean addAnnouncement(AnnouncementViewModel announcementToAdd)
        {
            var newAnnouncement = new Announcement();

            newAnnouncement.announcement = announcementToAdd.Announcement;
            newAnnouncement.userId = announcementToAdd.UserId;
            newAnnouncement.dateCreate = DateTime.Now;

            _db.Announcement.Add(newAnnouncement);
            _db.SaveChanges();

            return true;
        }

        public AnnouncementViewModel editAnnouncement(AnnouncementViewModel announcementToChange)
        {
            // Færsla, sem á að breyta, sótt í gagnagrunninn
            var query = (from announcement in _db.Announcement
                         where announcement.announcementId == announcementToChange.AnnouncementId
                         select announcement).SingleOrDefault();

            // Nýjar upplýsingar settar inn
            query.announcement = announcementToChange.Announcement;
            query.dateCreate = announcementToChange.DateCreate;

            //Breytingar vistaðar í gagnagrunninn
            try
            {
                _db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                // TODO
            }
            return announcementToChange;
        }
    }
}