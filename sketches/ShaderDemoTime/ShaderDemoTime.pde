PImage img;
PShader shader;

void setup(){
  size(800,600, P2D);
  
  img = loadImage("panda.jpg");  
  imageMode(CENTER);
  
  shader = loadShader("frag.glsl", "vert.glsl");
}

void draw(){
  //background(100);
  
  float time = millis() / 1000.0;
  shader.set("time", time);
  
  pushMatrix();
  translate(mouseX, mouseY);
  scale(.25);
  image(img, mouseX, mouseY);
  popMatrix();
  filter(shader);
}
