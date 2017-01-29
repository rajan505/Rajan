var NYC = NYC || {};
NYC.MobileNav = {};

/**
 * Class NYC.MobileNav
 *
 */

NYC.MobileNav = Class.extend({

    init: function init(options, elem) {

        console.log("init mobile nav");

		// Set element vars
		this.idNavTopSearches 		= $('#nav-top-searches');
		this.idToggleMobileSearch 	= $('#toggle-mobile-search');
		this.idNav 					= $('#nav');
		this.idNavHr 				= $('#nav-hr');
		this.idHeaderLinks 			= $('#header-links');

        this.options = $.extend({}, this.options, options);

        this.bindEvents();
    },

    options: {
        el: "mobile"
    },

	resizeElements : function() {
		var self = this;

		// Deactivate tablet search
		if ((($(document).width() < 768) || ($(document).width() > 839) ) && ($('#toggle-search-wide').hasClass('toggle-search-wide-background-arrow'))) {
				$("#global-search-form2").toggleClass("hidden");
				$('#toggle-search-wide').addClass('toggle-search-wide-background-ico-search').removeClass('toggle-search-wide-background-arrow');
				self.idNavHr.addClass('hidden');
		}
	},

    bindEvents: function (range) 
	{
		var self = this;
		var app = {};
		var _init = nav_open = false;

        // normalize vendor prefixes
		var doc = document.documentElement,
			inner = document.getElementById('inner-wrap');
		
		// Cache elements
		var html 				= $('html'),
			toggleSearchWide 	= $("#toggle-search-wide"),
			searchForm			= $("#global-search-form2"),
			navBtn				= $('#nav-open-btn');
		
		
        // top searches: show / hide
        self.idToggleMobileSearch.on("click", function () 
		{
            self.idNavTopSearches.toggleClass("hidden-phone");
            self.idToggleMobileSearch.toggleClass("nav-sprite-mobile");
			self.idToggleMobileSearch.find("span").toggleClass("hidden");
			searchForm.toggleClass("hidden");
        });

        // on iPad
        toggleSearchWide.on("click", function() 
		{
           searchForm.toggleClass("hidden");

            if ($(this).hasClass('toggle-search-wide-background-ico-search')) 
			{
                $(this).addClass('toggle-search-wide-background-arrow').removeClass('toggle-search-wide-background-ico-search');
				self.idNavHr.removeClass('hidden');
            } 
			else 
			{
                $(this).addClass('toggle-search-wide-background-ico-search').removeClass('toggle-search-wide-background-arrow');
				self.idNavHr.addClass('hidden');
            }
        });

        // helper function
        var hasParent = function (el, id) 
		{
            if (el) 
			{
                do {
                    if (el.id === id) {
                        return true;
                    }
                    if (el.nodeType === 9) {
                        break;
                    }
                }
                while ((el = el.parentNode));
            }
            return false;
        };

		// Transition
        var transform_prop = window.Modernizr.prefixed('transform'),
            transition_prop = window.Modernizr.prefixed('transition'),
            transition_end = (function() 
			{
				var props = {
                    'WebkitTransition' : 'webkitTransitionEnd',
                    'MozTransition'    : 'transitionend',
                    'OTransition'      : 'oTransitionEnd otransitionend',
                    'msTransition'     : 'MSTransitionEnd',
                    'transition'       : 'transitionend'
                };

                return props.hasOwnProperty(transition_prop) ? props[transition_prop] : false;
            })();


		// Close nav
		var closeNavEnd = function (e) 
		{
			if (e && e.target === inner) {
				document.removeEventListener(transition_end, closeNavEnd, false);
			}
			nav_open = false;
		};

		app.closeNav = function () 
		{
			if (nav_open) {
				
				// close navigation after transition or immediately
				var duration = (transition_end && transition_prop) ? parseFloat(window.getComputedStyle(inner, '')[transition_prop + 'Duration']) : 0;
				
				if (duration > 0) {
					document.addEventListener(transition_end, closeNavEnd, false);
				} else {
					closeNavEnd(null);
				}
			}
			// Remove js-nav class
			html.removeClass('js-nav');
		};
		
		app.openNav = function () 
		{
			if (nav_open) {
				return;
			}
			
			if (self.idNav.hasClass('hidden-phone')) {
				self.idNav.toggleClass("hidden-phone");
			}
			if (self.idHeaderLinks.hasClass('hidden-phone')) {
				self.idHeaderLinks.toggleClass("hidden-phone");
			}
			if (!self.idNavTopSearches.hasClass('hidden-phone')) {
				self.idNavTopSearches.toggleClass("hidden-phone");
				self.idToggleMobileSearch.toggleClass("nav-sprite-mobile");
				self.idToggleMobileSearch.find("span").toggleClass("hidden");
			}

			// Add js-nav class
			html.addClass('js-nav');
			nav_open = true;
		};

		app.toggleNav = function (e) 
		{
			if (nav_open && $('html').hasClass('js-nav')) {
				app.closeNav();
			} else {
				app.openNav();
			}
			if (e) {
				e.preventDefault();
			}
		};

		// open nav with main "nav" button
		navBtn.on("click", function (e) 
		{
			e.preventDefault();
			app.toggleNav();
		});

		// close nav by touching the partial off-screen content
		$(document).on("click", function (e) 
		{
			if ((e.target.id != 'nav-open-btn' ) && (nav_open) && (!hasParent(e.target, 'nav')) ) {
				e.preventDefault();
				app.closeNav();
			}
		});

		// Add js-nav class
		html.addClass('js-ready');

		$(window).smartresize(function() {
			self.resizeElements();
		});

    }
});
