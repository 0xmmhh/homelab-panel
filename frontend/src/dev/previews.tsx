import { ComponentPreview, Previews } from '@react-buddy/ide-toolbox'
import { ExampleLoaderComponent, PaletteTree } from './palette'

const ComponentPreviews = () => {
  return (
    <Previews palette={<PaletteTree />}>
      <ComponentPreview path="/PaletteTree">
        <PaletteTree />
      </ComponentPreview>
      <ComponentPreview path="/ExampleLoaderComponent">
        <ExampleLoaderComponent />
      </ComponentPreview>
    </Previews>
  )
}

export default ComponentPreviews
