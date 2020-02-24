using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using DataLayer.Enums;
using DataLayer.Interface.Entities;
using DataLayer.Models;
using DataLayer.Persitence;
using ErrorHandler;

namespace BussinesLayer
{
    public class ProxyBussines : IProxy
    {
        public Guid Guid { get; set; }
        public string DateSabt { get; set; }
        public bool Status { get; set; }
        public string Server { get; set; }
        public int Port { get; set; }
        public string Secret { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public ProxyType Type { get; set; }
        public string TypeName => Type.GetDisplay();
        public string Ping
        {
            get
            {
                var tt = false;
                try
                {
                    if (!Status) return "ندارد";
                    var ts = new Thread(new ThreadStart(async () => tt = await PingHost(Server, Port)));
                    ts.Start();
                    return tt ? "دارد" : "ندارد";
                }
                catch
                {
                    return "ندارد";
                }
            }
        }
        public static async Task<List<ProxyBussines>> GetAllAsync()
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Proxy.GetAll();
                return Mappings.Default.Map<List<ProxyBussines>>(a);
            }
        }
        private static async Task<bool> PingHost(string strIP, int intPort)
        {
            var blProxy = false;
            try
            {
                var client = new TcpClient(strIP, intPort);
                blProxy = true;
            }
            catch (SocketException)
            {
                return false;
            }
            catch
            {
                return false;
            }
            return blProxy;
        }
        public async Task SaveAsync()
        {
            try
            {
                using (var _context = new UnitOfWorkLid())
                {
                    var a = Mappings.Default.Map<Proxy>(this);
                    var res = _context.Proxy.Save(a);

                    _context.Set_Save();
                    _context.Dispose();

                }
            }
            catch (Exception exception)
            {
                WebErrorLog.ErrorInstence.StartErrorLog(exception);
            }
        }
        public static ProxyBussines Get(Guid guid)
        {
            using (var _context = new UnitOfWorkLid())
            {
                if (guid == System.Guid.Empty) return null;
                var a = _context.Proxy.Get(guid);
                return Mappings.Default.Map<ProxyBussines>(a);
            }
        }
        public static ProxyBussines Change_Status(Guid accGuid, bool status)
        {
            using (var _context = new UnitOfWorkLid())
            {
                var a = _context.Proxy.Change_Status(accGuid, status);
                return Mappings.Default.Map<ProxyBussines>(a);
            }
        }
    }
}
