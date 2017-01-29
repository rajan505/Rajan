var NYC = NYC || {};

/**
 * Class NYC.Global
 *
 */
NYC.Global = Class.extend({

    init : function init(options, elem) {

        console.log("init global");

        this.options = $.extend({}, this.options, options); 

        var mainNav = new NYC.MainNav();
		var self = this;

        if( $(".module-first-visit-alert").length != 0 ) {
          var firstVisitAlert = new NYC.FirstVisitAlert();
        }

        //no mobile nav for ie7 or ie8
        if( !$("html").hasClass("lt-ie9") ) {
            var mobileNav = new NYC.MobileNav();
        }
		
		//Add exitlink to external scripts
		if(location.href.indexOf("social-media.page")==-1&&location.href.indexOf("social-media.html")==-1&&location.href.indexOf("/programs.page")==-1&&location.href.indexOf("/programs.html")==-1){
			$("div.initiative").each(function(){
				if(!self.findNYCgovURL($(this).attr("data-link"))) {
					$(this).addClass("exitlink")
				}
			});
			$("a").each(function(index) {
				if(!self.findNYCgovURL($(this).attr("href"))) {
					$(this).addClass("exitlink")
				}
			});
		}
		
        this.fixPlaceHoldertext();
        this.initLanguageSelector();
        this.bindPrintButtons();
		this.initExitLinkOverlay();
		this.initTextSize();
    },

    options : {},
    
	// text size link trigger
	initTextSize : function() {
		
		// Overflow hooks for colorbox
		$(document).bind('cbox_open', function () {
			$('body').css({ overflow: 'hidden' });
		}).bind('cbox_closed', function () {
			$('body').css({ overflow: 'auto' });
		});
		
		$(document).on('click', '.text-size', function(e){
			e.preventDefault();

			// inject alert
			$.colorbox({
				onLoad: function(){
					// remove close button
					//$('#cboxClose').remove();
				},
				transition: 'none',
				html: function() {
					
		        	var html = ''+
						'<div class="leavingmsg">' +
							'<div class="richtext">' +
								'<p>To change the text size on NYC.gov you can use your web browser\'s settings. Most browsers include functionality to let you increase or decrease the text on a web page. For example, to increase text size using:</p>' +
								'<h4>Chrome</h4>' +
								'<p>In the menu to the right of the address bar, select and set Zoom level. Menu &gt; Zoom &gt; +</p>' +
								'<h4>Firefox</h4>' +
								'<p>In the View menu, select Zoom. View &gt; Zoom &gt; Zoom In</p>' +
								'<h4>Internet Explorer</h4>' +
								'<p>In the View menu, select Text Size. View &gt; Text Size &gt; Largest</p>' +
								'<h4>Safari</h4>' +
								'<p>In the View menu, select Zoom In. View &gt; Zoom In' +
								'<br/>Macintosh Shortcut: Command+</p>' +
								'<h4>No Web Browser Endorsement</h4>' +
								'<p>Common browsers are included in this page; mention of a specific browser does not imply endorsement or recommendation.</p>' +
							'</div>' +
						'</div>'
					
					return html; 
		      	}
			});
		});
	},
	
	// exit link trigger
	initExitLinkOverlay : function() {
		
		// Overflow hooks for colorbox
		$(document).bind('cbox_open', function () {
			$('body').css({ overflow: 'hidden' });
		}).bind('cbox_closed', function () {
			$('body').css({ overflow: 'auto' });
		});
		
		$(document).on('click', '.exitlink', function(e){
			//e.preventDefault();
			e.stopPropagation();
			// Get location
			var href = $(this).attr('href');
			
			// Set timer
			if (href && href != '' && href!="#") {
				setTimeout(function() {
					window.location = href;
				},3000);
			}
			
			//Set timer to close colobox incase the window href target is not self
			setTimeout(parent.$.colorbox.close, 5000);

			// inject alert
			$.colorbox({
				onLoad: function(){
					// remove close button
					$('#cboxClose').remove();
				},
				transition: 'none',
				html: function() {
					
		        	var html = ''+
						'<div class="leavingmsg">' +
							'<div>' +
								'<a href=""><img src="/assets/home/images/global/nyc.png" alt="leaving nyc.gov" /></a>' +
							'</div>' +
							'<h1>You are leaving the City of New York&#39;s website</h1>' +
							'<p>The City of New York does not imply approval of the listed destinations, warrant the accuracy of any information set out in those destinations, or endorse any opinions expressed therein or any goods or services offered thereby. Like NYC.gov, all other web sites operate under the auspices and at the direction of their respective owners who should be contacted directly with questions regarding the content of these sites.</p>' +
							'<p><b>You will be redirected within 5 seconds, assuming that the target server is available.</b></p>' +
						'</div>'
					
					return html; 
		      	}
			});
		});
	},

	//find internal NYC.gov URL
	findNYCgovURL: function(url) {
		//console.log("URL value - ", url)
		//console.count("URL Check");	
		if((url == undefined)||(url == null) || (url == " ") || (url == "")){
			return true;
		}
		if(url.match(/^https?:\/\/.*\.*nyc\.gov/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*nyc\.gov/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*csc\.nycnet/i)) {
			return true;
		}
		if(url.match(/^https:\/\/.*\.*csc\.nycnet/i)) {
			return true;
		}	
		if(url.match(/^http:\/\/.*\.*nycid\.nycnet/i)) {
			return true;
		}	
		if(url.match(/^https:\/\/.*\.*nycid\.nycnet/i)) {
			return true;
		}	
		if(url.match(/^http:\/\/.*\.*nycgovparks\.org/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*nyctmc\.org/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*hpdnyc\.org/i)) {
			return true;
		}	
		if(url.match(/^https:\/\/.*\.*hpdnyc\.org/i)) {
			return true;	
		}
		if(url.match(/^https:\/\/.*\.*secure24\.ipayment\.com/i)) {
			return true;
		}
		if(url.match(/^https:\/\/.*\.*paydirect\.link2gov\.com/i)) {
			return true;
		}
		if(url.match(/^https:\/\/.*\.*cityofnewyork\.us/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*cityofnewyork\.us/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*nycha\.info/i)) {
			return true;
		}		
		if(url.match(/^https:\/\/.*\.*nycha\.info/i)) {
			return true;
		}
		if(url.match(/^http:\/\/.*\.*nychaparking\.com/i)) {
			return true;
		}								
		if(url.match(/^http:\/\/.*\.*opportunitynycha\.org/i)) {
			return true;
		}				
		if(url.match(/^http:\/\/.*\.*studionycha\.org/i)) {
			return true;
		}						
		if(url.match(/^http:\/\/.*\.*revisionprospectplaza\.com/i)) {
			return true;
		}
		if(url.match(/^https:\/\/.*\.*nyc\.retirementpartner\.com/i)) {
			return true;
		}						
		if(url.match(/^http:\/\/.*\.*nyceplans\.org/i)) {
			return true;
		}							
		if(url.match(/^https:\/\/.*\.*nyceplans\.org/i)) {
			return true;
		}							
		if(url.match(/^http:\/\/.*\.*prod2\.moneytree\.com/i)) {
			return true;
		}							
		if(url.match(/^https:\/\/.*\.*prod2\.moneytree\.com/i)) {
			return true;
		}							
		if(url.match(/^https:\/\/.*\.*a068-acswebsrv\.nyc\.gov/i)) {
			return true;
		}							
		if(url.match(/^10.239.130.179:9111/i)) {
			return true;
		}							
		if(url.match(/^https:\/\/.*\.*a068-preventivesse\.nyc\.gov/i)) {
			return true;
		}							
								
		if(url.match(/^http:\/\/.*\.*fdnysmart\.org/i)) {
			return true;			
		}
		if(url.match(/^http:\/\/.*\.*maps.arcgis\.com/i)) {
			return true;			
		}	
		if(url.match(/^http:\/\/.*\.*nypdcrimestoppers\.com/i)) {
			return true;			
		}								
		if(url.match(/^\.\.\/\.\.\//i)) {
			return true;
		}
		if(url.match(/^\//i)) {
			return true;
		}
		if(url.match(/^javascript:/i)) {
			return true;
		}
		if(url.match(/^#/i)) {
			return true;
		}
		if(url.match(/^mailto:/i)) {
			return true;
		}
		if(url.match(/^tel:/i)) {
			return true;
		}
		if(url.match(/^\?pager.offset=/i)) {
			return true;
		}		
		return false;
	},

    //google translator styling overwrites
    initLanguageSelector : function() {

        setTimeout(function() {
            $(".main-header .language-selector .goog-te-menu-value span:first-child").text("Translate");
            $(".main-header .language-selector").show();
        }, 3000);

    },

    //fix placeholder issue for browser which don't support it
    fixPlaceHoldertext : function() {

        $('[placeholder]').focus(function() {
          var input = $(this);
          if (input.val() == input.attr('placeholder')) {
            input.val('');
            input.removeClass('placeholder');
          }
        }).blur(function() {
          var input = $(this);
          if (input.val() == '' || input.val() == input.attr('placeholder')) {
            input.addClass('placeholder');
            input.val(input.attr('placeholder'));
          }
        }).blur().parents('form').submit(function() {
          $(this).find('[placeholder]').each(function() {
            var input = $(this);
            if (input.val() == input.attr('placeholder')) {
              input.val('');
            }
          })
        });

    },

    initMobileNav : function() {
    	var mobileNav = new MobileNav({
    		el : "#nav",
    		whatever : ","
    	});
    },

    bindPrintButtons : function() {
      $('body').on('click', 'div.print-event', function(e) {
         //just print the whole page
        window.print();
      });
    },

    // get values of URL query strings
    getQueryString : function(sVar) {
      var urlStr = window.location.search.substring(1);
      var sv = urlStr.split("&");
      var i;
      for (i=0; i<sv.length; i+=1) {
        var ft = sv[i].split("=");
        if(ft[0] === sVar) {
          return ft[1];
        }
      }
    },

    // get values of URL hashes
    getLocationHash : function() {
      return window.location.hash.substring(1);
    },

    // create, read, delete cookies
    cookieManager : {
      prefix: "nyc",
      createCookie : function(name, value, days) {
        if (days) {
          var date = new Date();
          date.setTime(date.getTime()+(days*24*60*60*1000));
          var expires = "; expires="+date.toGMTString();
        }
        else var expires = "";
        document.cookie = this.prefix + "-" + name + "=" + encodeURIComponent(JSON.stringify(value)) + expires + "; path=/";
      },
      readCookie : function(name) {
        var nameEQ = this.prefix + "-" + name + "=";
        var ca = document.cookie.split(';');
        for(var i=0, l=ca.length; i < l; i++) {
          var c = ca[i];
          while (c.charAt(0)==' ') c = c.substring(1,c.length);
          if (c.indexOf(nameEQ) == 0) {
            var result = decodeURIComponent(c.substring(nameEQ.length,c.length));
            try {
              result = JSON.parse(result);
            } catch (err) {}
            return result;
          }
        }
        return null;
      },
      deleteCookie : function(name) {
        this.createCookie(name,'',-10);
      }
    }

});

$(document).ready(function(){
    var nycGlobal = new NYC.Global();
});

window.onunload = function(){
	$('.main-header input:text').val("");
	$('footer input:text').val("");
}
;
