using System;
using System.Collections.Generic;
using System.Linq;
using VTP2015.DataAccess.Bamaflex;

namespace VTP2015.Repositories.Remote_Services
{
    public class BamaflexRepository : IBamaflexRepository
    {
        private readonly Facade _client = new Facade();

        public BamaflexRepository( )
        {
        }

        public string GetPartimNameBySuperCode(string supercode)
        {
            return _client.GetPartimInformatie(supercode)[0].Partim.Naam;
        }

        public string GetModuduleNameBySuperCode(string supercode)
        {
            return _client.GetPartimInformatie(supercode)[0].Module.Naam;
        }

        public string GetOpleidingByStudentId(string id)
        {
            return _client.GetStudent(id.Split('|')[0]).Departementen[0].Opleidingen[0].Naam;
        }

        public string GetAfstudeerRichtingByStudentId(string id, string academieJaar)
        {
            return _client.GetStudentTraject(id.Split('|')[0], academieJaar).Partims.First(partim => partim.Afstudeerrichting.Naam != null).Afstudeerrichting.Naam;
        }

        public bool IsSuperCodeFromStudent(string superCode, string studentId, string academieJaar)
        {
            return GetPartimInformatieListByStudentId(studentId.Split('|')[0], academieJaar).Count(x => x.Supercode.Supercode1 == superCode) > 0;
        }

        private IEnumerable<PartimInformatie> GetPartimInformatieListByStudentId(string studentId, string academieJaar)
        {
            return _client.GetStudentTraject(studentId.Split('|')[0], academieJaar).Partims;
        }

        public IEnumerable<Opleiding> GetOpleidingen()
        {
            return _client.GetOpleidingen().ToList();
        }


        public string GetDocentFromPartim(string superCode)
        {
            return "docent@howest.be";
            // doesnt work
            return _client.GetDocentenByOpleidingsOnderdeel(superCode)[0];
        }

        IEnumerable<Entities.Opleiding> IBamaflexRepository.GetOpleidingen()
        {
            throw new NotImplementedException();
        }

        public ICollection<OpleidingsProgramma> GetKeuzeTrajecten(Entities.Opleiding opleiding)
        {
            return _client.GetOpleidingsprogrammaByOpleidingscode(opleiding.code).KeuzeTrajecten;
        }

        public PartimInformatie GetPartimInformationBySupercode(string supercode)
        {
            return _client.GetPartimInformatie(supercode)[0];
        }
    }
}