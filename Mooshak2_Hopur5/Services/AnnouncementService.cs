using Mooshak2_Hopur5.Utilities;
using Mooshak2_Hopur5.Models.Entities;
using Mooshak2_Hopur5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Data.Entity.Validation;

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
            //Sæki tilkynningu með ákveðnu ID ofan í gagnagrunn
            var announcement = _db.Announcement.SingleOrDefault(x => x.announcementId == announcementId);

            //Kasta villu ef ekki fannst tilkynning með þessu ID-i
            if (announcement == null)
            {
                //TODO: Kasta villu
            }

            //Set tilkynningu inn í ViewModelið

            var viewModel = new AnnouncementViewModel
            {
                DateCreate = announcement.dateCreate,
                Announcement = announcement.announcement
            };

            //Returna ViewModelinu með tilkynningunni í
            return viewModel;
        }

        public AnnouncementViewModel getAllAnnouncements()
        {
            //Sæki allar tilkynningar í töfluna
            var announcements = (from a in _db.Announcement
                                 orderby a.dateCreate descending
                                 select a).ToList();

            //Bý til lista af tilkynningum(AnnouncementViewModel)
            List<AnnouncementViewModel> announcementList;
            announcementList = new List<AnnouncementViewModel>();

            //Loopa í gegnum listann úr gagnagrunninum og setja inn í tilkynninga listann
            foreach (var entity in announcements)
            {
                var result = new AnnouncementViewModel
                {
                    Announcement = entity.announcement,
                    DateCreate = entity.dateCreate
                };

                announcementList.Add(result);
            }

            //Bý til nýtt AnnouncementViewModel og set listann inn í það
            AnnouncementViewModel viewModel = new AnnouncementViewModel
            {
                AnnouncementList = announcementList
            };

            //Returna viewModelinu með listanum
            return viewModel;
        }

        //Bæta við tilkynningu
        public Boolean addAnnouncement(AnnouncementViewModel announcementToAdd)
        {
            var newAnnouncement = new Announcement();

            //setja propery-in
            newAnnouncement.announcement = announcementToAdd.Announcement;
            newAnnouncement.userId = announcementToAdd.UserId;
            newAnnouncement.dateCreate = DateTime.Now;
            //Todo setja inn öll property

            //try
            //{
            //Vista ofan í gagnagrunn
            _db.Announcement.Add(newAnnouncement);
                _db.SaveChanges();
                return true;
            //}
            //catch
            //{
            //    return false;
            //}
        }

        //Breyta tilkynningu
        public AnnouncementViewModel editAnnouncement(AnnouncementViewModel announcementToChange)
        {
            // Sæki færsluna sem á að breyta í gagnagrunninn
            var query = (from announcement in _db.Announcement
                         where announcement.announcementId == announcementToChange.AnnouncementId
                         select announcement).SingleOrDefault();

            // Set inn breyttu upplýsingarnar
            query.announcement = announcementToChange.Announcement;
            query.dateCreate = announcementToChange.DateCreate;
            //Todo setja inn rest

            //Vista breytingar í gagnagrunn
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