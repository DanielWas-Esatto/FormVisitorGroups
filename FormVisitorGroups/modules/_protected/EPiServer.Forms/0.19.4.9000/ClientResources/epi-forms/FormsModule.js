﻿define([
// dojo
    'dojo/_base/declare',
    'dojo/_base/lang',

    'dojo/when',
// dijit
    'dijit/Destroyable',
// epi
    'epi/_Module',
    'epi/dependency',
    'epi/routes',

    'epi-cms/store/CustomQueryEngine',
// epi-addons
    'epi-forms/ModuleSettings',
    'epi-forms/widget/FormsContentTypeService'
],
function (
// dojo
    declare,
    lang,

    when,
// dijit
    Destroyable,
// epi
    _Module,
    dependency,
    routes,

    CustomQueryEngine,
// epi-addons
    ModuleSettings,
    FormsContentTypeService
) {

    return declare([_Module, Destroyable], {

        _settings: null,

        constructor: function (/*Object*/settings) {
            this._settings = settings;
        },

        initialize: function () {
            // summary:
            //      Initialize module
            // tags:
            //      public, extensions

            this.inherited(arguments);

            declare.safeMixin(ModuleSettings, this._settings);

            var registry = this.resolveDependency('epi.storeregistry'),
                profile = dependency.resolve('epi.shell.Profile');

            registry.create('epi-forms.formselement', this._getRestPath('formselement'), { queryEngine: CustomQueryEngine });
            registry.create('epi-forms.formsdata', this._getRestPath('formsdata'), { idProperty: 'id' });
            registry.create('epi-forms.externalfeed', this._getRestPath('externalfeed'));
            registry.create('epi-forms.visitordatasource', this._getRestPath('visitordatasource'));

            return when(profile.getContentLanguage(), lang.hitch(this, function (language) {
                // Set up the ContentTypeService 
                this.registerDependency('epi.cms.ContentTypeService', new FormsContentTypeService());
            }));
        },

        _getRestPath: function (/*String*/name) {
            // summary:
            //      Get EPiServer Forms REST path
            // name: [String]
            //      The current store name
            // tags:
            //      private

            return routes.getRestPath({
                moduleArea: 'EPiServer.Forms',
                storeName: name
            });
        }

    });

});