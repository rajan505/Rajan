var NYC = NYC || {};
NYC.MainNav = {};
/**
 * Class NYC.MainNav
 *
 */
NYC.MainNav = Class.extend({

    init : function init(options, elem) {

        console.log("init main nav");

        this.options = $.extend({}, this.options, options); 
      
        this.bindEvents();
    },

    options : {},

    bindEvents : function(range) {

        var self = this;

        //ie add active class on mousedown 
        $('.main-header nav ul li')
			.mousedown(function() {
				$(this).addClass("active");
			})
			.mouseup(function() {
				$(this).removeClass("active");
			})
			.click(function(e) {
				e.preventDefault();
				window.location.href = $(this).find("a").attr("href");
			});
    }

});



