import System.Linq;
 
  
function Start()
{

  var taggedObjects = GameObject.FindGameObjectsWithTag("way");
  var pathNumber = 3;
  var path3 = taggedObjects
        .OrderBy(function(g) g.name)
        .Select(function(g) g.GetComponent(WayPointScript).path[pathNumber-1])
        .ToArray();
        
  for(var go in path3)
  {
      go.GetComponent.<Renderer>().material.color = Color.red;
  }
}