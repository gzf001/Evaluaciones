using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Evaluaciones.Web.UI.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region Css

            //CSS
            StyleBundle theme = new StyleBundle("~/Content/theme/assets");

            StyleBundle jqueryUIcss1121 = new StyleBundle("~/Content/jqueryUIcss1121");

            StyleBundle bootstrap = new StyleBundle("~/Content/bootstrap");

            StyleBundle adminTools = new StyleBundle("~/Content/assets/admin-tools");

            StyleBundle sweetalert = new StyleBundle("~/Content/sweetalert");

            StyleBundle layoutLogin = new StyleBundle("~/Content/css/layout");

            StyleBundle dataTables_bootstrap = new StyleBundle("~/Content/css/dataTables_bootstrap");

            StyleBundle dataTables_plugins = new StyleBundle("~/Content/css/dataTables_plugins");

            StyleBundle glyphicons = new StyleBundle("~/Content/glyphicons-pro");

            StyleBundle iconsweets2 = new StyleBundle("~/Content/iconsweets2");

            StyleBundle toolTip = new StyleBundle("~/Content/tooltips");

            StyleBundle magnific_popup = new StyleBundle("~/Content/magnific_popup");

            StyleBundle nestable = new StyleBundle("~/Content/nestable");

            StyleBundle fancytree = new StyleBundle("~/Content/fancytree");

            StyleBundle summernoteCss = new StyleBundle("~/Content/summernoteCss");

            StyleBundle bootstrap_editable = new StyleBundle("~/Content/bootstrap_editable");

            StyleBundle address = new StyleBundle("~/Content/address");

            StyleBundle typeahead_bootstrap = new StyleBundle("~/Content/typeahead_bootstrap");

            bundles.Add(bootstrap.Include("~/Content/bootstrap/css/bootstrap.min.css"));

            bundles.Add(jqueryUIcss1121.Include("~/Content/js/jquery_ui/jquery-ui-themes-1.12.1/jquery-ui.min.css"));

            bundles.Add(theme.Include("~/Content/theme/assets/skin/default_skin/css/theme.min.css"));

            bundles.Add(adminTools.Include("~/Content/theme/assets/admin-tools/admin-forms/css/admin-forms.min.css"));

            bundles.Add(sweetalert.Include("~/Content/js/bootstrap.sweetalert/sweetalert.css"));

            bundles.Add(layoutLogin.Include("~/Content/css/layout.css"));

            bundles.Add(dataTables_bootstrap.Include("~/Content/theme/vendor/plugins/datatables/media/css/dataTables.bootstrap.css"));

            bundles.Add(dataTables_plugins.Include("~/Content/theme/vendor/plugins/datatables/media/css/dataTables.plugins.css"));

            bundles.Add(glyphicons.Include("~/Content/theme/assets/fonts/glyphicons-pro/glyphicons-pro.css"));

            bundles.Add(iconsweets2.Include("~/Content/theme/assets/fonts/iconsweets/iconsweets.css"));

            bundles.Add(toolTip.Include("~/Content/js/tooltipster-master/css/tooltipster.bundle.min.css"));

            bundles.Add(magnific_popup.Include("~/Content/theme/vendor/plugins/magnific/magnific-popup.css"));

            bundles.Add(nestable.Include("~/Content/theme/vendor/plugins/nestable/nestable.css"));

            bundles.Add(fancytree.Include("~/Content/theme/vendor/plugins/fancytree/skin-win8/ui.fancytree.min.css"));

            bundles.Add(summernoteCss.Include("~/Content/theme/vendor/plugins/summernote/summernote.css"));

            bundles.Add(bootstrap_editable.Include("~/Content/theme/vendor/plugins/xeditable/css/bootstrap-editable.css"));

            bundles.Add(address.Include("~/Content/theme/vendor/plugins/xeditable/inputs/address/address.css"));

            bundles.Add(typeahead_bootstrap.Include("~/Content/theme/vendor/plugins/xeditable/inputs/typeaheadjs/lib/typeahead.js-bootstrap.css"));
            
            #endregion

            #region JS

            //JS Vendor
            ScriptBundle jquery = new ScriptBundle("~/Content/jquery");

            ScriptBundle jqueryUI = new ScriptBundle("~/Content/jquery_ui");

            ScriptBundle bootstrapJS = new ScriptBundle("~/Content/bootstrapJS");

            ScriptBundle utility = new ScriptBundle("~/Content/theme/utility");

            ScriptBundle demo = new ScriptBundle("~/Content/theme/demo");

            ScriptBundle main = new ScriptBundle("~/Content/theme/main");

            ScriptBundle sweetalertJS = new ScriptBundle("~/Content/sweetalertJS");

            ScriptBundle layoutJS = new ScriptBundle("~/Content/layoutJS");

            ScriptBundle monthpicker = new ScriptBundle("~/Content/monthpicker");

            ScriptBundle timepicker = new ScriptBundle("~/Content/timepicker");

            ScriptBundle spectrum = new ScriptBundle("~/Content/spectrum");

            //Debe ser implmentado directamente en la página que lo requiera
            ScriptBundle steps = new ScriptBundle("~/Content/steps");

            ScriptBundle masks = new ScriptBundle("~/Content/Masks");

            ScriptBundle dataTables = new ScriptBundle("~/Content/dataTables");

            ScriptBundle tableTools = new ScriptBundle("~/Content/dataTables.tableTools");

            ScriptBundle colReorder = new ScriptBundle("~/Content/dataTables.colReorder");

            ScriptBundle dataTables_bootstrapJS = new ScriptBundle("~/Content/dataTables_bootstrapJS");

            ScriptBundle toolTipJS = new ScriptBundle("~/Content/toolTipJS");

            ScriptBundle magnific_popupJS = new ScriptBundle("~/Content/magnific_popupJS");

            ScriptBundle jqueryValidate = new ScriptBundle("~/Content/jqueryValidate");

            ScriptBundle jqueryNestable = new ScriptBundle("~/Content/jqueryNestable");

            ScriptBundle jqueryFancyTree = new ScriptBundle("~/Content/jqueryFancyTree");

            ScriptBundle summernoteJs = new ScriptBundle("~/Content/summernoteJs");

            bundles.Add(jquery.Include("~/Content/js/jquery-3.2.0.min.js"));

            bundles.Add(jqueryUI.Include("~/Content/js/jquery_ui/jquery-ui.min.js"));

            bundles.Add(bootstrapJS.Include("~/Content/bootstrap/js/bootstrap.js"));

            bundles.Add(utility.Include("~/Content/theme/assets/js/utility/utility.js"));

            bundles.Add(demo.Include("~/Content/theme/assets/js/demo/demo.js"));

            //ADMINS UTILITIES & INPACT CUSTOM
            bundles.Add(main.Include("~/Content/theme/assets/js/utility/functions.js"));

            bundles.Add(main.Include("~/Content/theme/assets/js/main.js"));

            bundles.Add(main.Include("~/Content/theme/assets/js/custom.js"));

            bundles.Add(sweetalertJS.Include("~/Content/js/bootstrap.sweetalert/sweetalert.min.js"));

            bundles.Add(layoutJS.Include("~/Content/js/layout.js"));

            bundles.Add(toolTipJS.Include("~/Content/js/tooltipster-master/js/tooltipster.bundle.min.js"));

            bundles.Add(magnific_popupJS.Include("~/Content/theme/vendor/plugins/magnific/jquery.magnific-popup.min.js"));

            bundles.Add(jqueryNestable.Include("~/Content/theme/vendor/plugins/nestable/jquery.nestable.js"));

            bundles.Add(jqueryFancyTree.Include("~/Content/theme/vendor/plugins/fancytree/jquery.fancytree-all.js"));

            //ADMIN FORMS JS
            bundles.Add(monthpicker.Include("~/Content/theme/assets/admin-tools/admin-forms/js/source/jquery-ui-monthpicker.js"));
            bundles.Add(timepicker.Include("~/Content/theme/assets/admin-tools/admin-forms/js/source/jquery-ui-timepicker.js"));
            bundles.Add(spectrum.Include("~/Content/theme/assets/admin-tools/admin-forms/js/source/jquery.spectrum.js"));
            bundles.Add(steps.Include("~/Content/theme/assets/admin-tools/admin-forms/js/source/jquery.steps.js"));
            bundles.Add(jqueryValidate.Include("~/Content/theme/assets/admin-tools/admin-forms/js/jquery.validate.min.js"));

            //MASKS
            bundles.Add(masks.IncludeDirectory("~/Content/theme/assets/js/utility/masks", "*.js", true));
            bundles.Add(masks.Include("~/Content/theme/vendor/plugins/jquerymask/jquery.maskedinput.min.js"));

            //TABLES
            bundles.Add(dataTables.Include("~/Content/theme/vendor/plugins/datatables/media/js/jquery.dataTables.js"));
            bundles.Add(tableTools.Include("~/Content/theme/vendor/plugins/datatables/extensions/TableTools/js/dataTables.tableTools.min.js"));
            bundles.Add(colReorder.Include("~/Content/theme/vendor/plugins/datatables/extensions/ColReorder/js/dataTables.colReorder.min.js"));
            bundles.Add(dataTables_bootstrapJS.Include("~/Content/theme/vendor/plugins/datatables/media/js/dataTables.bootstrap.js"));

            //Editor
            bundles.Add(summernoteJs.Include("~/Content/theme/vendor/plugins/summernote/summernote.js"));

            //Local JS

            #region Administracion

            ScriptBundle aplicacion = new ScriptBundle("~/js/aplicaciones");
            ScriptBundle menuItem = new ScriptBundle("~/js/menuItem");
            ScriptBundle rol = new ScriptBundle("~/js/rol");
            ScriptBundle permiso = new ScriptBundle("~/js/permiso");
            ScriptBundle usuario = new ScriptBundle("~/js/usuarios");
            ScriptBundle empresa = new ScriptBundle("~/js/empresas");
            ScriptBundle centroCosto = new ScriptBundle("~/js/centrosCosto");
            ScriptBundle usuariosConectados = new ScriptBundle("~/js/usuariosConectados");

            bundles.Add(aplicacion.Include("~/Content/js/admin/aplicaciones/aplicaciones.js"));
            bundles.Add(menuItem.Include("~/Content/js/admin/menuItem/menuItem.js"));
            bundles.Add(rol.Include("~/Content/js/admin/rolPermiso/roles.js"));
            bundles.Add(permiso.Include("~/Content/js/admin/rolPermiso/permisos.js"));
            bundles.Add(usuario.Include("~/Content/js/admin/usuarios/usuarios.js"));
            bundles.Add(empresa.Include("~/Content/js/admin/empresas/empresas.js"));
            bundles.Add(centroCosto.Include("~/Content/js/admin/centrosCosto/centrosCosto.js"));
            bundles.Add(usuariosConectados.Include("~/Content/js/admin/usuariosConectados/usuariosConectados.js"));

            #endregion

            #region Banco de preguntas

            ScriptBundle bancoPregunta = new ScriptBundle("~/js/bancoPregunta");

            bundles.Add(bancoPregunta.Include("~/Content/js/bancoPreguntas/bancoPreguntas.js"));

            #endregion

            #endregion
        }
    }
}