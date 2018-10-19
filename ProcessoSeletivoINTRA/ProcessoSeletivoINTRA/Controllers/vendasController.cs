using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProcessoSeletivoINTRA.Models;

namespace ProcessoSeletivoINTRA.Controllers
{
    public class vendasController : Controller
    {
        private INDRA_BDEntities db = new INDRA_BDEntities();

        // GET: vendas
        public ActionResult Index()
        {
            var venda = db.venda.Include(v => v.produto1);
            return View(venda.ToList());
        }

        // GET: vendas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // GET: vendas/Create
        public ActionResult Create()
        {
            ViewBag.produto = new SelectList(db.produto, "id_produto", "nome");
            
            return View();
        }

        // POST: vendas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_venda,produto,valor,data_venda,quantidade")] venda venda)
        {
            if (ModelState.IsValid)
            {
                db.venda.Add(venda);
                db.SaveChanges();
                new produtoesController().EditEstoque(venda.produto, venda.quantidade);
                return RedirectToAction("Index");
            }

            ViewBag.produto = new SelectList(db.produto, "id_produto", "nome", venda.produto);
            return View(venda);
        }

        // GET: vendas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            ViewBag.produto = new SelectList(db.produto, "id_produto", "nome", venda.produto);
            return View(venda);
        }

        // POST: vendas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_venda,produto,valor,data_venda,quantidade")] venda venda)
        {
            if (ModelState.IsValid)
            {
                db.Entry(venda).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.produto = new SelectList(db.produto, "id_produto", "nome", venda.produto);
            return View(venda);
        }

        // GET: vendas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            venda venda = db.venda.Find(id);
            if (venda == null)
            {
                return HttpNotFound();
            }
            return View(venda);
        }

        // POST: vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            venda venda = db.venda.Find(id);
            db.venda.Remove(venda);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
