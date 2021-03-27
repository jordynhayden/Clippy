using Clippy.Data;
using Clippy.Entities;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Clippy.Pages.Search
{
    public class IndexModel : PageModel
    {
        private ClippyContext _context;

        public IndexModel(ClippyContext context) {
            _context = context;
        }

        public IList<Bookmark> Bookmarks { get; set; }

        public string Query { get; set; }

        public async Task OnGetAsync(string query)
        {
            string githubId = "";
            foreach (Claim claim in User.Claims)
            {
                if (claim.Type == ClaimTypes.NameIdentifier) githubId = claim.Value;
            }

            int id = (await _context.GetUserByGithubId(githubId)).Id; 
            Bookmarks = await _context.GetBookmarksBySearch(query, id);
            Query = query;
        }
    }
}